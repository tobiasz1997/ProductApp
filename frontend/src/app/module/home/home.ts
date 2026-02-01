import {ChangeDetectionStrategy, Component, inject, signal} from '@angular/core';
import {Product} from '../../core/api/models/product';
import {ProductApiService} from '../../core/api/services/product-api.service';
import {TableLazyLoadEvent, TableModule} from 'primeng/table';
import {finalize} from 'rxjs';
import {FavouriteButton} from './components/favourite-button/favourite-button';
import {CurrencyPipe} from '@angular/common';

@Component({
  selector: 'app-home',
  imports: [
    TableModule,
    FavouriteButton,
    CurrencyPipe
  ],
  templateUrl: './home.html',
  styleUrl: './home.scss',
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class Home {
  public products = signal<Product[]>([]);
  public isProductLoading = signal(false);

  public page = signal(0)
  public pageSize = signal(10);
  public total = signal(0);

  private _productApiService = inject(ProductApiService);

  onLazyLoad(event: TableLazyLoadEvent): void {
    const first = event.first ?? 0;
    const rows = event.rows ?? this.pageSize();

    this.page.set(Math.floor(first / rows))
    this.pageSize.set(rows);

    this.getProducts();
  }

  private getProducts(): void {
    this.isProductLoading.set(true);
    this._productApiService.getProducts(
      this.page()
    )
      .pipe(
        finalize(() => {
          this.isProductLoading.set(false)
        })
      )
      .subscribe(res => {
        this.products.set(res.products)
        this.total.set(res.total)
      })
  }
}
