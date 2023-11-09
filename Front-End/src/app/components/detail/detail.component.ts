import { Component } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ODataProductResponse } from 'src/app/interfaces/o.data.product.response';
import { ProductsService } from 'src/app/services/products/products.service';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { DetailEditCommentComponent } from '../share/dialog/detail-edit-comment/detail-edit-comment.component';
import { DetailDeleteCommentComponent } from '../share/dialog/detail-delete-comment/detail-delete-comment.component';
import { TokenService } from 'src/app/services/token/token.service';
import { ProductUserService } from 'src/app/services/product.user/product.user.service';
import { ProductUser } from 'src/app/interfaces/product.user';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';
import { ProductEditComponent } from '../share/dialog/product-edit/product-edit.component';
import { ProductDisableComponent } from '../share/dialog/product-disable/product-disable.component';
import { BreakpointObserver } from '@angular/cdk/layout';

export interface Comment {
  name: string;
  titleReview: string;
  updated: Date;
  comment: string;
}

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})
export class DetailComponent {
  response: ODataProductResponse | undefined;
  operating: boolean = false;
  userReview: ProductUser | undefined;
  effectiveReviews: ProductUser[] | undefined;
  reviewTitle: string = "";
  review: string = "";
  reviewTitleStart: string = "";
  reviewStart: string = "";
  breakpoint: number = (window.innerWidth <= 800) ? 1 : 2;
  admin: boolean = this.authService.isAdmin();

  constructor(private fb: FormBuilder, public dialog: MatDialog, private prodService: ProductsService, private route: ActivatedRoute, private prodUserService: ProductUserService, private authService: AuthenticationService, private router: Router,private observer: BreakpointObserver,) {
    this.operating = true;
    this.fetch();
  }

  onResize(event: any) {
    this.breakpoint = (event.target.innerWidth <= 800) ? 1 : 2;
  }

  fetch(){
    this.prodService.oDataFetch("$expand=category,productUsers($expand=user)&$count=true&$filter=isDisabled eq false and productId eq " + this.route.snapshot.params['id']).subscribe((val) => {
      this.response = val;
      this.userReview = this.response.value[0].productUser ? 
        this.response.value[0].productUser.find((val) => val.userId === this.authService.getUserId()) ? 
        this.response.value[0].productUser.find((val) => val.userId === this.authService.getUserId()) 
        : {
            productId: this.response.value[0].productId, 
            dateAdded: new Date(), 
            isUsed: false, 
            inWishList: false, 
            isDisabled: false, 
            lastModifiedDate: new Date(), 
            product: undefined, 
            review: null, 
            reviewTitle: null,
            user: undefined,
            userId: ""
          } 
        : undefined;
      this.effectiveReviews = this.response!.value[0].productUser!.filter((val) => val.review !== null && val.reviewTitle !== null);
        this.reviewTitle = (this.userReview!.reviewTitle ? this.userReview!.reviewTitle : "") + "";
        this.review = (this.userReview!.review ? this.userReview!.review : "") + "";
        this.reviewStart = (this.userReview!.review ? this.userReview!.review : "") + "";
        this.reviewTitleStart = (this.userReview!.reviewTitle ? this.userReview!.reviewTitle : "") + "";
      this.operating = false;
    });
  }

  onReset(){
    this.operating = true;
    this.prodUserService.delete(this.response!.value[0].productId).subscribe({
      next: () =>{
        this.fetch();
      },
      error: (error) => {
        console.log(error);
        this.fetch();
      }
    });
  }

  onSubmit(){
    this.operating = true;
    this.prodUserService.review(this.response!.value[0].productId, this.review, this.reviewTitle).subscribe({
      next: () =>{
        this.fetch();
      },
      error: (error) => {
        console.log(error);
        this.fetch();
      }
    });
  }

  handleUsed(){
    this.operating = true;
    this.prodUserService.setUsed(this.response!.value[0].productId).subscribe({
      next: () =>{
        this.fetch();
      },
      error: (error) => {
        console.log(error);
        this.fetch();
      }
    });
  }

  handleWishlist(){
    this.operating = true;
    this.prodUserService.setWishlist(this.response!.value[0].productId).subscribe({
      next: () =>{
        this.fetch();
      },
      error: (error) => {
        console.log(error);
        this.fetch();
      }
    });
  }




  openEditDialog() {
    const dialogRef = this.dialog.open(ProductEditComponent);
    dialogRef.afterClosed().subscribe(result => {
      // console.log(`${result}`);
    });
  }

  openDisableDialog() {
    this.operating = true;
    const dialogRef = this.dialog.open(ProductDisableComponent);
    dialogRef.afterClosed().subscribe({
      next: (result) => 
      {
        if (result){
          this.prodService.disable(this.response!.value[0], this.response!.value[0].productId).subscribe(() =>{
            this.router.navigateByUrl("/products");
          });
      }
      },
      error: (error) => {
        this.operating = false;
        console.log(error)
      }
    });
  }


  gridListRowHeight!:number
  ngOnInit() {
    this.observer.observe(['(max-width: 800px)']).subscribe((screenSize) => {
      if (screenSize.matches) {
        this.gridListRowHeight = 300;
      } else {
        this.gridListRowHeight = 400;
      }
    });
  }
}
