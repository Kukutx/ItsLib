import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { ODataProductResponse } from 'src/app/interfaces/o.data.product.response';
import { ProductsService } from 'src/app/services/products/products.service';
import { BreakpointObserver } from '@angular/cdk/layout';
import { ProductCreaComponent } from '../share/dialog/product-crea/product-crea.component';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent {
  title : string = 'Prodotti';
  response: ODataProductResponse | undefined;
  filter: string = "";
  constructor(public dialog: MatDialog,private prodService: ProductsService, private observer: BreakpointObserver){
    this.fetch();
  }

  openDialog() {
    this.dialog.open(ProductCreaComponent);
  }

  fetch(){
    let aplFlt = "";
    if (this.filter.trim() !== "") {
      aplFlt = " and (contains(tolower(name), tolower('" + this.filter.trim() + "')) or contains(tolower(category/name), tolower('" + this.filter.trim() + "'))";
      if (!Number.isNaN(parseFloat(this.filter.trim()))) aplFlt += " or contains(tolower(introductoryPrice), tolower(" + this.filter.trim() + "))";
      aplFlt += ")"
    }
    this.prodService.oDataFetch("$expand=category&$count=true&$top=" + this.pageSize + "&$skip=" + (this.pageSize*this.pageIndex) + "&$filter=isDisabled eq false " + aplFlt).subscribe((val) =>{
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
