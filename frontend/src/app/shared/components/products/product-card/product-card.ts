import {ChangeDetectionStrategy, Component, input} from '@angular/core';
import {Product} from '../../../../core/api/models/product';
import {FavouriteProductButton} from '../favourite-product-button/favourite-product-button';
import {Button} from 'primeng/button';

@Component({
  selector: 'app-product-card',
  imports: [
    FavouriteProductButton,
    Button
  ],
  templateUrl: './product-card.html',
  styleUrl: './product-card.scss',
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ProductCard {
  product = input.required<Product>();

  navigateToProductUrl(url: string) {
    window.open(url, '_blank');
  }
}
