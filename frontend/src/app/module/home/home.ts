import {ChangeDetectionStrategy, Component, inject, OnInit, Signal} from '@angular/core';
import {Product} from '../../core/api/models/product';
import {TableModule} from 'primeng/table';
import {ProductList} from '../../shared/components/products/product-list/product-list';
import {ProductsAmazonStore} from '../../core/store/products-amazon.store';

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
  private _productsAmazonStore = inject(ProductsAmazonStore);

  products: Signal<Product[]> = this._productsAmazonStore.products;
  loading: Signal<boolean> = this._productsAmazonStore.isLoading;

  ngOnInit() {
    this._productsAmazonStore.getProducts();
  }
}
