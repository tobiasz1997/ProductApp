import {Component, DestroyRef, inject, OnInit, signal} from '@angular/core';
import {ProductList} from '../../shared/components/products/product-list/product-list';
import {Product} from '../../core/api/models/product';
import {finalize, startWith} from 'rxjs';
import {FavouritesService} from '../../core/services/favourites.service';
import {takeUntilDestroyed, toSignal} from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-favourite-products',
  imports: [
    ProductList
  ],
  templateUrl: './favourite-products.html',
  styleUrl: './favourite-products.scss',
  standalone: true
})
export class FavouriteProducts implements OnInit {
  private _favouritesApiService = inject(FavouritesService);
  private _destroyRef = inject(DestroyRef);

  loading = signal(false);
  products = toSignal<Product[] | undefined>(this._favouritesApiService.favourites$.pipe(startWith([])));

  ngOnInit() {
    this.getProducts();
  }

  private getProducts(): void {
    this.loading.set(true);
    this._favouritesApiService.getFavourites()
      .pipe(
        takeUntilDestroyed(this._destroyRef),
        finalize(() => {
          this.loading.set(false)
        })
      ).subscribe();
  }
}
