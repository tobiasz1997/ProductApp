import {ChangeDetectionStrategy, Component, DestroyRef, inject, OnInit, signal} from '@angular/core';
import {Product} from '../../core/api/models/product';
import {ProductApiService} from '../../core/api/services/product-api.service';
import {TableModule} from 'primeng/table';
import {finalize} from 'rxjs';
import {ProductList} from '../../shared/components/products/product-list/product-list';
import {takeUntilDestroyed} from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-home',
  imports: [
    TableModule,
    ProductList
  ],
  templateUrl: './home.html',
  styleUrl: './home.scss',
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class Home implements OnInit {
  products = signal<Product[]>([]);
  loading = signal(false);

  private _productApiService = inject(ProductApiService);
  private _destroyRef = inject(DestroyRef);

  ngOnInit() {
    this.getProducts();
  }

  private getProducts(): void {
    this.loading.set(true);
    this._productApiService.getProducts()
      .pipe(
        takeUntilDestroyed(this._destroyRef),
        finalize(() => {
          this.loading.set(false)
        })
      )
      .subscribe(res => {
        this.products.set(res)
      })
  }
}
