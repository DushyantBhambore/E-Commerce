import { Component, inject, OnInit } from '@angular/core';
import { CartService } from '../../Service/cart.service';
import { CommonModule } from '@angular/common';
import { I, S } from '@angular/cdk/keycodes';
import { routes } from '../../app.routes';
import { Router } from '@angular/router';
import { AddressComponent } from '../address/address.component';
import { PaymentComponent } from '../payment/payment.component';
import { MatDialog } from '@angular/material/dialog';
import { state } from '@angular/animations';
import { count } from 'rxjs';

@Component({
  selector: 'app-view-cart',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './view-cart.component.html',
  styleUrl: './view-cart.component.css'
})
export class ViewCartComponent implements OnInit {

  cartservice = inject(CartService)
  cartdata :any

  userid = JSON.parse(sessionStorage.getItem('logindata') || '{}');
  id : number = this.userid.userId
  zipcode :number =this.userid.zipcode
  stateid :number =this.userid.stateId
  countryid :number =this.userid.countryId
  address :string = this.userid.address
  subtotal = 0;
  usrname = this.userid.username
  invoiceResponse :any

  cartvalue! :any

  constructor(private dialog: MatDialog) { }
  ngOnInit(): void {
    this.gotocart(this.id)

  }
  gotocart(id:number)
  {
    debugger
    console.log(id);
    this.cartservice.getcartbyid(id).subscribe((res:any)=>{
      console.log("res",res);
      this.cartdata = res.data
      console.log("cart",this.cartdata);
     this.calculateSubtotal();
    })

}

cartid! : number
removeFromCart(productId: number)
  {
    
    var cartobj ={
      productId : productId,
      userid : this.id
    }

    debugger
    this.cartservice.removeFromCart(cartobj).subscribe({
      next: (response) => {
        console.log('Product removed from cart:', response);
      
        alert('Product removed from cart successfully!');
      },
      error: (err) => {
        console.error('Error removing from cart:', err);
        alert('Failed to remove product from cart.');
      }
    });

  }

  calculateSubtotal() {
    this.subtotal = this.cartdata.reduce
    ((t : any, i: any) => t + i.sellingPrice * i.qty, 0);
  }

  router = inject(Router)
  onPay() {
       
    this.openPaymentDialog();
      }
    
  



  openPaymentDialog() {
    const dialogRef = this.dialog.open(PaymentComponent, {
      data: { subtotal: this.subtotal }
    });

    debugger
    dialogRef.afterClosed().subscribe((paymentDetails) => {
      if (paymentDetails) {
        this.cartservice.paymentcard(paymentDetails).subscribe((isValid) => {
          if (isValid) {
            alert('Payment successful!');
            this.generateInvoice();

          } else {
            alert('Invalid payment details.');
          }
        });
      }
    });
  }


  generateInvoice()
  {
    debugger
    var invoiceRequest = {
      userId: this.id,
      DeliveryAddress: this.address,
      zipcode: this.zipcode,
      stateId: this.stateid,
      countryId: this.countryid,
      Items: this.cartdata.map((item :any) => ({ productId: item.productId , qty: item.qty, sellingPrice: item.sellingPrice ,procuctcode : item.procuctcode}))
    }
    this.cartservice.getinvoice(invoiceRequest).subscribe({
      next: (response) => {
        this.invoiceResponse = response; // Store the response in a variable
        console.log('Invoice generated successfully:', this.invoiceResponse);
        // Optionally, redirect to a new page to display the receipt
        this.router.navigate(['/receipt'], { state: { invoiceData: this.invoiceResponse } });
      },
      error: (err) => {
        console.error('Error generating invoice:', err);
        alert('Failed to generate invoice.');
      }
    });

  }
} 