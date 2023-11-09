import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { AuthGuard } from './guards/auth/auth.guard';
import { ProductsComponent } from './components/products/products.component';
import { RegisterComponent } from './components/register/register.component';
import { AdminManagementComponent } from './components/admin.management/admin.management.component';
import { DetailComponent } from './components/detail/detail.component';
import { UserProfileComponent } from './components/user.profile/user.profile.component';
import { WishlistComponent } from './components/wishlist/wishlist.component';
import { LogoutComponent } from './components/logout/logout.component';

const routes: Routes = [
  {path: "login", component: LoginComponent},
  {path: "logout", component: LogoutComponent},
  {path: "register", component: RegisterComponent},
  {
    path: "", 
    component: HomeComponent,
    children: [
      {path: "products", component: ProductsComponent, canActivate: [AuthGuard]},
      {path: "product/:id", component: DetailComponent, canActivate: [AuthGuard]},
      {path: "wish-list", component: WishlistComponent, canActivate: [AuthGuard]},
      {path: "profile", component: UserProfileComponent, canActivate: [AuthGuard]},
      {path: "users/admin", component: AdminManagementComponent, canActivate: [AuthGuard]}
    ],
  },
  {path: "**", redirectTo: "products"}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
