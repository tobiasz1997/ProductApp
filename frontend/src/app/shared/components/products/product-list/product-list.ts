import {Component, input} from '@angular/core';
import {Product} from '../../../../core/api/models/product';
import {ProductCard} from '../product-card/product-card';
import {DataView} from 'primeng/dataview';

@Component({
  selector: 'app-product-list',
  imports: [
    ProductCard,
    DataView
  ],
  templateUrl: './product-list.html',
  styleUrl: './product-list.scss',
  standalone: true
})
export class ProductList {
  products = input.required<Product[] | undefined>();
  loading = input.required<boolean>();
}
