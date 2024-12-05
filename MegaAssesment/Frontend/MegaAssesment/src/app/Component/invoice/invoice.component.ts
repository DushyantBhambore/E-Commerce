import { Component } from '@angular/core';
import { CartService } from '../../Service/cart.service';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-invoice',
  standalone: true,
  imports: [ReactiveFormsModule,CommonModule],
  templateUrl: './invoice.component.html',
  styleUrl: './invoice.component.css'
})
export class InvoiceComponent {

  invoiceForm: FormGroup;

  constructor(private fb: FormBuilder, private invoiceService: CartService) {
    this.invoiceForm = this.fb.group({
      userId: [null, Validators.required],
      deliveryAddress: ['', Validators.required],
      deliveryZipcode: [null, Validators.required],
      deliveryState: [null, Validators.required],
      deliveryCountry: [null, Validators.required],
      items: this.fb.array([this.createCartItem()])
    });
  }

  get items(): FormArray {
    return this.invoiceForm.get('items') as FormArray;
  }

  createCartItem(): FormGroup {
    return this.fb.group({
      productId: [null, Validators.required],
      productCode: ['', Validators.required],
      saleQty: [null, [Validators.required, Validators.min(1)]],
      sellingPrice: [null, [Validators.required, Validators.min(0)]]
    });
  }

 
  onSubmit(): void {
    if (this.invoiceForm.invalid) {
      alert('Form is invalid. Please check all fields.');
      return;
    }

    const salesMasterDto: any = this.invoiceForm.value;
    this.invoiceService.getinvoice(salesMasterDto).subscribe({
      next: (response) => {
        alert(response);
        console.log('Invoice created:', response);
      },
      error: (error) => {
        alert('Error creating invoice. Please try again.');
        console.error(error);
      }
    });
  }




 

}
