import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';

export interface Comment {
  name: string;
  titleReview: string;
  updated: Date;
  comment: string;
}

@Component({
  selector: 'app-detail-edit-comment',
  templateUrl: './detail-edit-comment.component.html',
  styleUrls: ['./detail-edit-comment.component.scss']
})
export class DetailEditCommentComponent {
  constructor(private fb: FormBuilder) {
  }

  noteForm = this.fb.group({
    titleReview: [''],
    updated: new Date('2/20/16'),
    comment: [''],
  });

  onSubmit() {
    const value = this.noteForm.value;
    const comment: Comment = {
      name: "nameTest",
      titleReview: value.titleReview!,
      updated: value.updated!,
      comment: value.comment!
    };
  }

}
