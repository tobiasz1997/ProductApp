import {ChangeDetectionStrategy, Component, inject, OnInit, Signal} from '@angular/core';
import {ProductList} from '../../shared/components/products/product-list/product-list';
import {Product} from '../../core/api/models/product';
import {FavouritesStore} from '../../core/store/favourites.store';

@Component({
  selector: 'app-favourite-products',
  imports: [
    ProductList
  ],
  templateUrl: './favourite-products.html',
  styleUrl: './favourite-products.scss',
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FavouriteProducts implements OnInit {
  private _favouriteStore = inject(FavouritesStore);

  loading: Signal<boolean> = this._favouriteStore.isGetLoading;
  products: Signal<Product[]> = this._favouriteStore.favourites;

  ngOnInit() {
    this._favouriteStore.getFavourites()
  }
}
