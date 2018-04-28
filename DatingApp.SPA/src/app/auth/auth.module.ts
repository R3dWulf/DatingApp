import { NgModule } from '@angular/core';
import { Http, RequestOptions } from '@angular/http';
import { AuthHttp, AuthConfig } from 'angular2-jwt';

export function authHttpServiceFactory(http: Http, options: RequestOptions) {
  return new AuthHttp(new AuthConfig({
  tokenName: 'token',
  tokenGetter: (() => localStorage.getItem('token')),
  globalHeaders: [{'Content-Type': 'application/json'}],
  }), http, options);
}

// I HAD FORGOTTEN THE PROVIDERS!!
@NgModule({
  providers: [
    {
      provide: AuthHttp,
      useFactory: authHttpServiceFactory,
      deps: [Http, RequestOptions]
    }
  ]
})
export class AuthModule {}
