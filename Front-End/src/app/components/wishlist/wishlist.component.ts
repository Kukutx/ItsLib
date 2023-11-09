import { Component } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { ODataProductResponse } from 'src/app/interfaces/o.data.product.response';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';
import { ProductsService } from 'src/app/services/products/products.service';
import { BreakpointObserver } from '@angular/cdk/layout';

@Component({
  selector: 'app-wishlist',
  templateUrl: './wishlist.component.html',
  styleUrls: ['./wishlist.component.scss']
})
export class WishlistComponent {

  title : string = 'Prodotti';
  response: ODataProductResponse | undefined;
  filter: string = "";
  constructor(private prodService: ProductsService, private observer: BreakpointObserver, private authService: AuthenticationService){
    this.fetch();
  }

  fetch(){
    let aplFlt = "";
    if (this.filter.trim() !== "") {
      aplFlt = " and (contains(tolower(name), tolower('" + this.filter.trim() + "')) or contains(tolower(category/name), tolower('" + this.filter.trim() + "'))";
      if (!Number.isNaN(parseFloat(this.filter.trim()))) aplFlt += " or contains(tolower(introductoryPrice), tolower(" + this.filter.trim() + "))";
      aplFlt += ")";
    }
    this.prodService.oDataFetch("$expand=category,productUsers&$count=true&$top=" + this.pageSize + "&$skip=" + (this.pageSize*this.pageIndex) + "&$filter=isDisabled eq false and (productUsers/any(c:c/userId eq '" + this.authService.getUserId() + "' and c/inWishList))" + aplFlt).subscribe((val) =>{
      this.response = val;
    });
  }

  applyFilter(event: Event) {
    this.filter = (event.target as HTMLInputElement).value;
    this.fetch();
  }

  reloadProducts() {
    this.fetch();
  }



  pageSize = 10;
  pageIndex = 0;
  pageSizeOptions = [5, 10, 25];

  hidePageSize = false;
  showPageSizeOptions = true;
  showFirstLastButtons = true;
  disabled = false;

  pageEvent: PageEvent | undefined;

  handlePageEvent(e: PageEvent) {
    this.pageEvent = e;
    this.pageSize = e.pageSize;
    this.pageIndex = e.pageIndex;
    this.fetch();
  }


  dxTileViewItemHeight!: number;
  dxTileViewItemWidth!: number;
  ngOnInit() {
    this.observer.observe(['(max-width: 800px)']).subscribe((screenSize) => {
      if (screenSize.matches) {
        this.dxTileViewItemHeight = 150;
        this.dxTileViewItemWidth = 150;
      } else {
        this.dxTileViewItemWidth = 210;
        this.dxTileViewItemHeight = 210;
      }
    });
  }
  
}



