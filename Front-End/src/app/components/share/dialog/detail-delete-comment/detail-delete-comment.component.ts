import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-detail-delete-comment',
  templateUrl: './detail-delete-comment.component.html',
  styleUrls: ['./detail-delete-comment.component.scss']
})
export class DetailDeleteCommentComponent {

  message: string = "Are you sure want to delete?"
  constructor(private dialogRef: MatDialogRef<DetailDeleteCommentComponent>) {
  }

  onConfirmClick(): void {
      this.dialogRef.close(true);
  }
}
