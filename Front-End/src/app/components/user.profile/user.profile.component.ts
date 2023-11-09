import { Component } from '@angular/core';

export interface DiscountCode {
  discountCode: string;
  percentage : string;
}



@Component({
  selector: 'app-user.profile',
  templateUrl: './user.profile.component.html',
  styleUrls: ['./user.profile.component.scss']
})
export class UserProfileComponent {
  title : string = 'Profile';
  hide = true;

  isEditing = false;

  discountCodes: DiscountCode[] = [
    {
      discountCode: '666-666-666',
      percentage : '50%'
    },
    {
      discountCode: '666-666-666',
      percentage : '50%'
    },
    {
      discountCode: '666-666-666',
      percentage : '50%'
    },
  ];
}
