import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { SelectionModel } from '@angular/cdk/collections';
import { ODataProductResponse } from 'src/app/interfaces/o.data.product.response';
import { TokenService } from 'src/app/services/token/token.service';
import { UserOp } from 'src/app/interfaces/user.op';
import { UserService } from 'src/app/services/user/user.service';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';
import { Router } from '@angular/router';

export interface PeriodicElement {
  name: string;
  position: number;
}

const ELEMENT_DATA: PeriodicElement[] = [
  {position: 1, name: 'Hydrogen'},
  {position: 2, name: 'Helium'},
  {position: 3, name: 'Lithium'},
  {position: 4, name: 'Beryllium'},
  {position: 5, name: 'Boron'},
  {position: 6, name: 'Carbon'},
  {position: 7, name: 'Nitrogen'},
  {position: 8, name: 'Oxygen'},
  {position: 9, name: 'Fluorine'},
  {position: 10, name: 'Neon'},
  {position: 11, name: 'Neon'},
  {position: 12, name: 'Neon'},
];



@Component({
  selector: 'app-admin.management',
  templateUrl: './admin.management.component.html',
  styleUrls: ['./admin.management.component.scss']
})
export class AdminManagementComponent{

  title: string = "Admin";
  admin: boolean = true;

  constructor(private userService: UserService, private authService: AuthenticationService, private router: Router){
    this.admin = authService.isAdmin();
    if (!this.admin) router.navigateByUrl("/products");
    
  }

  promoteSelected(){
    for (let i = 0; i < this.selection.selected.length; i++){
      if (this.selection.selected[i].userRoles!.findIndex((val) => val === "Admin") === -1){
        this.userService.promote(this.selection.selected[i].id).subscribe(()=>this.fetch());
      }
    }
  }

  demoteSelected(){
    let usrId = this.authService.getUserId();
    for (let i = 0; i < this.selection.selected.length; i++){
      if (this.selection.selected[i].id !== usrId && this.selection.selected[i].userRoles!.findIndex((val) => val === "Admin") !== -1){
        this.userService.demote(this.selection.selected[i].id).subscribe(()=>this.fetch());
      }
    }
    
  }

  disableSelected(){
    let usrId = this.authService.getUserId();
    for (let i = 0; i < this.selection.selected.length; i++){
      if (this.selection.selected[i].id !== usrId && !this.selection.selected[i].isDisabled){
        this.userService.disableOrEnable(this.selection.selected[i].id).subscribe(()=>this.fetch());
      }
    }
  }

  reableSelected(){
    let usrId = this.authService.getUserId();
    for (let i = 0; i < this.selection.selected.length; i++){
      if (this.selection.selected[i].id !== usrId && this.selection.selected[i].isDisabled){
        this.userService.disableOrEnable(this.selection.selected[i].id).subscribe(()=>this.fetch());
      }
    }
  }


  displayedColumns: string[] = ['select', 'name', 'surname', 'userRoles' ,'userName', 'fiscalCode', 'loyaltyCardCode', 'isDisabled'];
  dataSource = new MatTableDataSource<UserOp>();
  selection = new SelectionModel<UserOp>(true, []);

  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  toggleAllRows() {
    if (this.isAllSelected()) {
      this.selection.clear();
      return;
    }

    this.selection.select(...this.dataSource.data);
  }

  /** The label for the checkbox on the passed row */
  checkboxLabel(row?: UserOp): string {
    if (!row) {
      return `${this.isAllSelected() ? 'deselect' : 'select'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${this.dataSource.data.indexOf(row) + 1}`;
  }

  
  applyFilter(event: Event) {
  }

  @ViewChild(MatPaginator) paginator: MatPaginator | undefined;

  ngAfterViewInit() {
    this.fetch()
  }

  fetch(){
    this.userService.oDataFetch("$count=true&$top=" + this.pageSize + "&$skip=" + (this.pageIndex * this.pageSize)).subscribe(
      (val) => {
        let temp = val.value;
        for (let i = 0; i < temp.length; i++){
          this.userService.getRole(temp[i].id).subscribe((val) => {
            temp[i].userRoles = val;
          })
        }
        this.dataSource.data = temp;
        this.count = val.count;
      }
    );
  }
  count: number = 0;
  pageSize: number = 5;
  pageIndex: number = 0;

  handlePageEvent(event: PageEvent){
    this.pageSize = event.pageSize;
    this.pageIndex = event.pageIndex;
    
    this.fetch();
  }
}
