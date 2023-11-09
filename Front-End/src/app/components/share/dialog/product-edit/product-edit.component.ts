import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrls: ['./product-edit.component.scss']
})
export class ProductEditComponent {

  productForm = new FormGroup({
    nameProduct: new FormControl('', Validators.required),
    price: new FormControl('', Validators.required),
    category: new FormControl('', Validators.required),
  });

  productOptionalForm = new FormGroup({
    nameImage: new FormControl('', Validators.required),
    imageUrl: new FormControl('', [
      Validators.required,
      Validators.pattern('https://.*')])
  });


  submitForm() {
    if (this.productForm.valid) {
    }
  }
}
