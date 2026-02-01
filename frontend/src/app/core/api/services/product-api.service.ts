import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {map, Observable} from 'rxjs';
import {ProductResponse} from '../models/product-response';

@Injectable({
  providedIn: 'root'
})
export class ProductApiService {
  private readonly _apiUrl = 'https://dummyjson.com';
  private readonly _selectedData = 'select=id,title,category,price';
  private readonly _httpClient = inject(HttpClient);

  getProducts(page: number, pageSize = 10): Observable<ProductResponse> {
    const skip = page * pageSize;
    return this._httpClient
      .get<ProductResponse>(`${this._apiUrl}/products?limit=${pageSize}&skip=${skip}&${this._selectedData}`)
      .pipe(
        map((response: ProductResponse) => {
          return {
            ...response,
            products: response.products.slice(-pageSize)
          } as ProductResponse
        })
      )
  }
}
