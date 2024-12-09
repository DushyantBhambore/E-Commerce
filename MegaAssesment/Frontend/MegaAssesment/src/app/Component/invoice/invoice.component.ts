import { Component, inject, OnInit } from '@angular/core';
import { CartService } from '../../Service/cart.service';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule, JsonPipe } from '@angular/common';

@Component({
  selector: 'app-invoice',
  standalone: true,
  imports: [ReactiveFormsModule,CommonModule],
  templateUrl: './invoice.component.html',
  styleUrl: './invoice.component.css'
})
export class InvoiceComponent implements OnInit{

  invoiceForm: FormGroup;
  userid = JSON.parse(sessionStorage.getItem('logindata') || '{}');
  id : number = this.userid.userId

  getrecipt :any
  service = inject(CartService)
  getinvoice :any
  invoiceItems = JSON.parse(sessionStorage.getItem('invoce') || '')
  Sales = JSON.parse(sessionStorage.getItem('sales') || '')
Total :any


  getinvoicetable(id : number)
  {
    this.service.getinvoicebyid(id).subscribe((data) => {
      this.getinvoice = data
      this.Total = this.getinvoice.subtotal
      console.log(this.getinvoice);
    })
  }

  ngOnInit(): void {
    
    // this.getinvoice = this.invoiceItems
    // console.log(this.getinvoice);
    this.getinvoicetable(this.id)

  }


  getinvoceRecipt(reciptid : number)
  {
    this.OpenModal()
    this.service.getrecieptbyid(reciptid).subscribe((data) => {
      this.getrecipt = data
      console.log(this.getrecipt);
    })

  }
  CloseModal()
  {
    const modal = document.getElementById("myModal");
    if (modal) {
      modal.style.display = "none";
    }
  }

  OpenModal()
  {
    const modal = document.getElementById("myModal");
    if (modal) {
      modal.style.display = "block";
    }
  }

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
