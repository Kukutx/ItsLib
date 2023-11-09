import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot,  Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable, catchError, delay, of, switchMap } from 'rxjs';
import { TokenService } from 'src/app/services/token/token.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard{
  constructor (private router: Router, private tokenService: TokenService){}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    let token = this.tokenService.readToken();
    if (token === "") {
      this.router.navigate(["login"]);
      return false;
    } else if (this.tokenService.isExpired()){
      return this.tokenService.requestRefreshToken().pipe(
        switchMap(
          (val)=>{
            this.tokenService.setToken(val.token, val.expiration, val.refreshToken);
            return of(true);
          }
        ),
        catchError(
          () => {
            setTimeout(() => {}, 5000);
            if (this.tokenService.isExpired()){
              this.tokenService.logOut();
              this.router.navigate(["login"]);
              return of(false);
            }
            else return of(true)
          }
        )
      )
    }
    else return true;
  }
  
  
}
