import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Direction } from 'devextreme/common';

interface NewProductOptionalTemp {
  type:string,
  keyName:string,
  valueName:string,
}

export interface NewProductOptional{
  Image: Map<string,string>,
  Link:Map<string,string>,
  Number:Map<string,string>,
  Text:Map<string,string>,
}



@Component({
  selector: 'app-product-crea',
  templateUrl: './product-crea.component.html',
  styleUrls: ['./product-crea.component.scss']
})
export class ProductCreaComponent {
  productForm = new FormGroup({
    nameProduct: new FormControl('', Validators.required),
    price: new FormControl('', Validators.required),
    category: new FormControl('', Validators.required),
  });

  productOptionalForm: FormGroup;

  constructor(private formBuilder: FormBuilder) {
    this.productOptionalForm = this.formBuilder.group({
      productOptionalList: this.formBuilder.array([this.putNewProductOptional()]),
    });
  }
  
  putNewProductOptional() {
    return this.formBuilder.group({
      type: [''],
      keyName: [''],
      valueName: ['']   
    });
  }
  
  get productOptionalsArray() {
    return this.productOptionalForm.get('productOptionalList') as FormArray;
  }
  
  addNewOptional() {
    this.productOptionalsArray.push(this.putNewProductOptional());
  }
  
  removeOptional(index: number) {
    this.productOptionalsArray.removeAt(index);
  }


  newProductOptional: NewProductOptional = {
    Image: new Map<string, string>(),
    Link: new Map<string, string>(),
    Number: new Map<string, string>(),
    Text: new Map<string, string>(),
  };
  
  

  submitForm() {
    console.log(this.productForm.value)
    console.log(this.productOptionalForm.value.productOptionalList)

    const formValues = this.productOptionalForm.value.productOptionalList;

    formValues.forEach((formValue:NewProductOptionalTemp) => {
      console.log(11);
      switch(formValue.type) {
        case 'Image':
          console.log("11")
          this.newProductOptional.Image.set(formValue.keyName, formValue.valueName);
          break;
        case 'Link':
          this.newProductOptional.Link.set(formValue.keyName, formValue.valueName);
          break;
        case 'Number':
          this.newProductOptional.Number.set(formValue.keyName, formValue.valueName);
          break;
        case 'Text':
          this.newProductOptional.Text.set(formValue.keyName, formValue.valueName);
          break;
      }
    });
  
    console.log(this.newProductOptional);

    if (this.productForm.valid) {
    }
  }
}
