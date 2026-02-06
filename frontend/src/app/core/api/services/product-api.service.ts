import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {map, Observable} from 'rxjs';
import {AmazonProductResponse} from '../models/amazon-product-response';
import {environment} from '../../../../environments/environment';
import {Product} from '../models/product';

@Injectable({
  providedIn: 'root'
})
export class ProductApiService {
  private readonly _httpClient = inject(HttpClient);

  getProducts(page = 1, category = 'software', type = 'BEST_SELLERS', country = 'PL'): Observable<Product[]> {
    return this._httpClient
      .get<any>(`${environment.rapidApiUrl}/best-sellers?category=${category}&type=${type}&country=${country}&page=${page}`)
      .pipe(
        map((res: AmazonProductResponse | null) => {
          if (res) {
            const sellers = res.data.best_sellers ?? [];
            return sellers.map((seller) => {
              return {
                externalId: seller.asin,
                externalUrl: seller.product_url,
                photoUrl: seller.product_photo,
                rating: seller.product_star_rating,
                price: seller.product_price,
                title: seller.product_title,
                id: null
              } as Product
            })
          } else {
            return []
          }
        })
      )
  }
}
