import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { ODataProductResponse } from 'src/app/interfaces/o.data.product.response';
import { ODataUsers } from 'src/app/interfaces/o.data.users';
import { UserOp } from 'src/app/interfaces/user.op';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClient: HttpClient) { }

  oDataFetch(queryString: string): Observable<ODataUsers>{
    return this.httpClient.get<UserOp[]>(environment.apiBaseUrl + "odata/User?" + queryString).pipe(map(
      (val: any)=>{
        return {
          count: val["@odata.count"],
          value: val.value
        };
      }
    ));
  }

  getRole(id: string): Observable<string[]>{
    return this.httpClient.get<string[]>(environment.apiBaseUrl + "api/Authenticate/GetUserRole/"+ id).pipe(map((val: any) =>{
      return val.roles;
    }));
  }

  disableOrEnable(id: string): Observable<any>{
    return this.httpClient.put<any>(environment.apiBaseUrl + "api/Authenticate/Switch/" + id,{});
  }

  promote(id: string): Observable<any>{
    return this.httpClient.put<any>(environment.apiBaseUrl + "api/Authenticate/UserToAdmin/" + id,
    {});
  }

  demote(id: string): Observable<any>{
    return this.httpClient.put<any>(environment.apiBaseUrl + "api/Authenticate/AdminToUser/" + id,{});
  }
}
