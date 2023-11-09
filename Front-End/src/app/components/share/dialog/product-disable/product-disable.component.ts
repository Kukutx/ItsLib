import { Component } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-product-disable',
  templateUrl: './product-disable.component.html',
  styleUrls: ['./product-disable.component.scss']
})
export class ProductDisableComponent {
  message: string = "Are you sure want to disable?"
  constructor(private dialogRef: MatDialogRef<ProductDisableComponent>) {
  }

  onConfirmClick(): void {
      this.dialogRef.close(true);
  }
}
