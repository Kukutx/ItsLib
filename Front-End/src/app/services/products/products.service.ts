import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { AdditionalData } from 'src/app/interfaces/additional.data';
import { Category } from 'src/app/interfaces/category';
import { ODataProductResponse } from 'src/app/interfaces/o.data.product.response';
import { Product } from 'src/app/interfaces/product';
import { ProductUser } from 'src/app/interfaces/product.user';
import { environment } from 'src/environments/environment';
import { createPatch } from 'rfc6902';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {



  constructor(private httpClient: HttpClient) { }

  disable(o: Product, productId: string): Observable<any>{
    let copy = {...o};
    copy.isDisabled = true;
    for(let i = 0; i < copy.productUser!.length; i++){
      copy.productUser![i].isDisabled = true;
    }
    let patch = createPatch(o, copy);
    return this.httpClient.patch<any>(environment.apiBaseUrl + "api/Product/" + productId,patch);
  }

  oDataFetch(queryString: string): Observable<ODataProductResponse>{
    return this.httpClient.get<ODataProductResponse>(environment.apiBaseUrl + "odata/Product?" + queryString)
    .pipe(
      map(
        (res: any) => {
          let products: Product[] = [];
          for (let i = 0; i < res.value.length; i++){
            let prodUsers: ProductUser[] = [];
            if (res.value[i].productUsers){
              for (let j = 0; j < res.value[i].productUsers.length; j++){
                prodUsers[j] = {
                  dateAdded: new Date(new Date(res.value[i].productUsers[j].dateAdded).toUTCString()),
                  inWishList: res.value[i].productUsers[j].inWishList,
                  isDisabled: res.value[i].productUsers[j].isDisabled,
                  isUsed: res.value[i].productUsers[j].isUsed,
                  lastModifiedDate: new Date(new Date(res.value[i].productUsers[j].lastModifiedDate).toUTCString()),
                  productId: res.value[i].productUsers[j].productId,
                  userId: res.value[i].productUsers[j].userId,
                  review: res.value[i].productUsers[j].review,
                  reviewTitle: res.value[i].productUsers[j].reviewTitle,
                  user: res.value[i].productUsers[j].user,
                  product: undefined
                }
              }
            }
            try {
              let addData = JSON.parse(res.value[i].additionalData);
              products[i] = {
                productUser: res.value[i].productUsers ? [...prodUsers] : res.value[i].productUsers,
                categoryId: res.value[i].categoryId,
                dateAdded: new Date(new Date(res.value[i].dateAdded).toUTCString()),
                introductoryPrice: res.value[i].introductoryPrice,
                isDisabled: res.value[i].isDisabled,
                name: res.value[i].name,
                productId: res.value[i].productId,
                additionalData: JSON.parse(res.value[i].additionalData),
                category: res.value[i].category
              };
            } catch (error) {
              products[i] = {
                productUser: res.value[i].productUsers ? [...prodUsers] : res.value[i].productUsers,
                categoryId: res.value[i].categoryId,
                dateAdded: new Date(new Date(res.value[i].dateAdded).toUTCString()),
                introductoryPrice: res.value[i].introductoryPrice,
                isDisabled: res.value[i].isDisabled,
                name: res.value[i].name,
                productId: res.value[i].productId,
                additionalData: [],
                category: res.value[i].category
              };
            }
            
          }
          return {
            context: res["@odata.context"],
            count: res["@odata.count"],
            value: products
          };
        }
      )
    )
  }
}
