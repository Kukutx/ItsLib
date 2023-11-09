import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MatIconModule } from '@angular/material/icon';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule} from '@angular/material/core';
import { MatButtonToggleModule} from '@angular/material/button-toggle';
import { MatPaginatorModule} from '@angular/material/paginator';
import {MatGridListModule} from '@angular/material/grid-list';
import { MatDialogModule} from '@angular/material/dialog';
import { MatSortModule} from '@angular/material/sort';
import { MatTableModule} from '@angular/material/table';
import {MatRippleModule} from '@angular/material/core';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {MatExpansionModule} from '@angular/material/expansion';
import {MatSelectModule} from '@angular/material/select';


import { DxDataGridModule, DxTileViewModule } from 'devextreme-angular';
import { DxButtonModule } from 'devextreme-angular';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './components/app/app.component';
import { TokenService } from './services/token/token.service';
import { AuthInterceptor } from './interceptors/auth/auth.interceptor';
import { ProductsComponent } from './components/products/products.component';
import { SidenavComponent } from './components/sidenav/sidenav.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { AuthGuard } from './guards/auth/auth.guard';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AuthenticationService } from './services/authentication/authentication.service';
import { ProductsService } from './services/products/products.service';
import { DetailComponent } from './components/detail/detail.component';
import { AdminManagementComponent } from './components/admin.management/admin.management.component';
import { DetailEditCommentComponent } from './components/share/dialog/detail-edit-comment/detail-edit-comment.component';
import { DetailDeleteCommentComponent } from './components/share/dialog/detail-delete-comment/detail-delete-comment.component';
import { UserProfileComponent } from './components/user.profile/user.profile.component';
import { WishlistComponent } from './components/wishlist/wishlist.component';
import { ProductCreaComponent } from './components/share/dialog/product-crea/product-crea.component';
import { ProductEditComponent } from './components/share/dialog/product-edit/product-edit.component';
import { ProductDisableComponent } from './components/share/dialog/product-disable/product-disable.component';
import { LogoutComponent } from './components/logout/logout.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    ProductsComponent,
    SidenavComponent,
    RegisterComponent,
    AdminManagementComponent,
    DetailComponent,
    DetailEditCommentComponent,
    DetailDeleteCommentComponent,
    UserProfileComponent,
    WishlistComponent,
    ProductCreaComponent,
    ProductEditComponent,
    ProductDisableComponent,
    LogoutComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,

    MatIconModule,
    MatSidenavModule,
    MatToolbarModule,
    MatButtonModule,
    MatListModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatButtonToggleModule,
    MatGridListModule,
    MatDialogModule,
    MatTableModule, 
    MatSortModule,
    MatRippleModule,
    MatPaginatorModule,
    MatCheckboxModule,
    MatExpansionModule,
    MatSelectModule,

    DxTileViewModule,
    DxButtonModule,
    DxDataGridModule,
  ],
  providers: [
    TokenService,
    {
      useClass: AuthInterceptor,
      provide: HTTP_INTERCEPTORS,
      multi: true
    },
    AuthenticationService,
    ProductsService,
    AuthGuard,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
platformBrowserDynamic().bootstrapModule(AppModule);