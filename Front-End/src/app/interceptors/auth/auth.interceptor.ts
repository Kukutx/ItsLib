import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { BehaviorSubject, Observable, catchError, delay, filter, switchMap, take, throwError } from 'rxjs';
import { TokenService } from 'src/app/services/token/token.service';
import { Router } from '@angular/router';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private tokenService: TokenService, private router: Router) {}

  private addToken(request: HttpRequest<any>, token: string) {
    return request.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`,
      },
    });
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (request.url.includes("api/Authenticate/refresh-token")) return next.handle(request);
    let req = this.addToken(request, this.tokenService.readToken());

    return next.handle(req)
    .pipe(catchError(error => {
            if (error instanceof HttpErrorResponse && error.status === 401) {
                return this.handle401Error(request, next);
            } else {
                return throwError(() => error);
            }
        }));
  }

  private handle401Error(request: HttpRequest<any>, next: HttpHandler) {
    return this.tokenService.requestRefreshToken().pipe(
        switchMap((token) => {
          this.tokenService.setToken(token.token, token.expiration, token.refreshToken);
          return next.handle(this.addToken(request, token.token));
        }),
        catchError((error) => {
          if (error instanceof HttpErrorResponse && error.status === 400) {
            setTimeout(() => {}, 5000);
            if (this.tokenService.isExpired()){
              this.tokenService.logOut();
              this.router.navigate(["login"]);
            }
          }
          return throwError(() => error);
        })
    );
  }
}
