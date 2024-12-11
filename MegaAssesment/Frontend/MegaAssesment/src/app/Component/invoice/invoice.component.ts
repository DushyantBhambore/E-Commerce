import { Component, inject, OnInit } from '@angular/core';
import { CartService } from '../../Service/cart.service';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule, DatePipe, JsonPipe } from '@angular/common';
import { jsPDF } from "jspdf";
import html2canvas from 'html2canvas';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-invoice',
  standalone: true,
  imports: [ReactiveFormsModule,CommonModule,DatePipe],
  templateUrl: './invoice.component.html',
  styleUrl: './invoice.component.css'
})
export class InvoiceComponent implements OnInit{

  invoiceForm: FormGroup;
  // userid = JSON.parse(sessionStorage.getItem('logindata') || '{}');
  // id : number = this.userid.userId

  getrecipt :any
  service = inject(CartService)
  getinvoice :any
  Total :any

  id! : number
  userlogindata: any | null = null;  
  
 router = inject(Router)
  


  getinvoicetable(id : number)
  {
    this.service.getinvoicebyid(id).subscribe((data) => {
      this.getinvoice = data
      this.Total = this.getinvoice.subtotal
      console.log(this.getinvoice);
    })
  }

  ngOnInit(): void {
    
    debugger
    // this.getinvoice = this.invoiceItems
    // console.log(this.getinvoice);

    const storedData = sessionStorage.getItem('logindata');
    if (storedData) {
      this.userlogindata = JSON.parse(storedData);
      if (this.userlogindata) {
        this.id = this.userlogindata.userId;
      }
    }

    if (!this.userlogindata) {
      // Redirect to login if no user data is found
      this.router.navigate(['/dashboard']);
    }
  
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


  downloadPDF(): void {
    const invoiceElement = document.getElementById('myModal'); 

    if (invoiceElement) {
      
      const printButton = document.querySelector('.btn-danger');
      const exportButton = document.querySelector('.btn-success');
      const invocetext = document.querySelector('.modal-title');
      const close = document.querySelector('.close');
      const footer = document.querySelector('.modal-footer');
      if (printButton) printButton.setAttribute('style', 'display: none');
      if (exportButton) exportButton.setAttribute('style', 'display: none');
      if (invocetext) invocetext.setAttribute('style', 'display: none');
      if (close) close.setAttribute('style', 'display: none');
      if (footer) footer.setAttribute('style', 'display: none');


      html2canvas(invoiceElement, { scale: 2 }).then((canvas) => {
        
        if (printButton) printButton.removeAttribute('style');
        if (exportButton) exportButton.removeAttribute('style');

        const imgData = canvas.toDataURL('image/png'); 
        const doc = new jsPDF('p', 'mm', 'a4'); 

        const imgWidth = 190; 
        const imgHeight = (canvas.height * imgWidth) / canvas.width;

        doc.addImage(imgData, 'PNG', 10, 10, imgWidth, imgHeight); 
        doc.save('invoice.pdf');
        this.CloseModal(); 
      });
    }
  }
 
}
