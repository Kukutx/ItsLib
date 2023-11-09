import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { LoginResponse } from 'src/app/interfaces/login.response';
import { environment } from 'src/environments/environment';
import { Share } from '@ngspot/rxjs/decorators';

@Injectable({
  providedIn: 'root'
})
export class TokenService {
  private refreshToken: string | null;
  private token: string | null;
  private expiration: string | null;
  constructor(private httpClient: HttpClient, private router: Router){
      this.token = localStorage.getItem("token");
      this.expiration = localStorage.getItem("expiration");
      this.refreshToken = localStorage.getItem("refreshToken");
  }
  public setToken(token: string, expire: string, refreshToken: string){
    this.token = token;
    this.expiration = expire;
    this.refreshToken = refreshToken;
    localStorage.setItem("token", this.token);
    localStorage.setItem("expiration", this.expiration);
    localStorage.setItem("refreshToken", this.refreshToken);
  }

  public isExpired(): boolean{
    var temp = new Date(this.expiration!).toUTCString();
    var exp = new Date(temp);

    var now = new Date(Date.now());
    return exp.getTime() - now.getTime() <= 0;
  }

  public readToken(): string{
      if(this.expiration && this.token && this.refreshToken){

          return this.token;

      }else{

        this.token = "";
        this.expiration = "";
        this.refreshToken = "";
        this.logOut();

      }
      return this.token;
  }

  public logOut(){
    this.token = "";
    this.expiration = "";
    this.refreshToken = "";
    localStorage.removeItem("token");
    localStorage.removeItem("expiration");
    localStorage.removeItem("refreshToken");
  }

   @Share()
  public requestRefreshToken(): Observable<LoginResponse>{
    
    return this.httpClient.post<LoginResponse>(
      environment.apiBaseUrl + "api/Authenticate/refresh-token", 
      { 
        accessToken: this.token, 
        refreshToken: this.refreshToken
      }
    );
  }
}
