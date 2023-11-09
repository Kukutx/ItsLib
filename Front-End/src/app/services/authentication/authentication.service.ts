import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { LoginResponse } from 'src/app/interfaces/login.response';
import { User } from 'src/app/interfaces/user';
import { environment } from 'src/environments/environment';
import { TokenService } from '../token/token.service';
import * as jwt_decode from "jwt-decode";
@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  expDate: Date | undefined;
  constructor(private httpClient: HttpClient, private tokenService: TokenService) {
    
    let tryDate = localStorage.getItem("expDate");
    if (tryDate) {
      this.expDate = new Date(new Date(tryDate).toUTCString());
    }
    
  }

  public Login(email: string, password: string): Observable<LoginResponse> {
    return this.httpClient.post<LoginResponse>(environment.apiBaseUrl + "api/Authenticate/login", { username: email, password: password });
  }

  public Register(user: User): Observable<LoginResponse> {
    return this.httpClient.post<LoginResponse>(environment.apiBaseUrl + "api/Authenticate/register", user);
  }

  private getDecodedToken() {
    let token = this.tokenService.readToken();
    if (token !== "")
      return jwt_decode.jwtDecode(token);
    return "";
  }
  

  public isAdmin(): boolean {
    if (this.tokenService.readToken() === "") return false;
    const decodedToken = this.getDecodedToken();
    const roleString = Object.keys(decodedToken).filter((t) =>
      t.endsWith('/role')
    )[0];
    const str = JSON.parse(JSON.stringify(decodedToken.valueOf()))[roleString]
    if (typeof(str) === "string") {
      if (str === "Admin") return true;
    } else if ((str as []).find(v => v === "Admin")){
      return true;
    }
    return false;
  }

  public getUserId(): string {
    if (this.tokenService.readToken() === "") return "";
    const decodedToken = this.getDecodedToken();
    const roleString = Object.keys(decodedToken).filter((t) =>
      t.endsWith('/nameidentifier')
    )[0];
    const str = JSON.parse(JSON.stringify(decodedToken.valueOf()))[roleString]
    return str;
  }

  
}
