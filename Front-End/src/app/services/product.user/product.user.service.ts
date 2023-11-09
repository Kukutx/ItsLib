import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductUserService {

  constructor(private httpClient: HttpClient) { }

  setWishlist(productId: string): Observable<any>{
    return this.httpClient.post(environment.apiBaseUrl + "api/ProductUser/whislist",{ productId: productId});
  }

  setUsed(productId: string): Observable<any>{
    return this.httpClient.post(environment.apiBaseUrl + "api/ProductUser/used",{ productId: productId});
  }

  review(productId: string, review: string | null, reviewTitle: string | null): Observable<any>{
    return this.httpClient.put(environment.apiBaseUrl + "api/ProductUser/createreview",{ productId: productId, review: review, reviewTitle: reviewTitle});
  }

  delete(productId: string): Observable<any>{
    return this.httpClient.put(environment.apiBaseUrl + "api/ProductUser/DeleteReview/" + productId,{});
  }
}
