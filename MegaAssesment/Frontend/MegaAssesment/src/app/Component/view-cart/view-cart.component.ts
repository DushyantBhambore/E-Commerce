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
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-view-cart',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './view-cart.component.html',
  styleUrl: './view-cart.component.css'
})
export class ViewCartComponent implements OnInit {

  cartservice = inject(CartService)

  toastr = inject(ToastrService)
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

  isrecipient = false

  cartvalue! :any

  cartCount = 0
 

  constructor(private dialog: MatDialog, private cartService : CartService) { }
  ngOnInit(): void {
    this.gotocart(this.id)
    this.cartService.getCartCount().subscribe((count) => {
      this.cartCount = count;
    });

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

// remove by id
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
      this.toastr.success('Product removed from cart.', 'Success', {
        timeOut: 3000,
        progressBar: true,
        progressAnimation: 'increasing',
        positionClass: 'toast-top-right'
        
      });
        this.gotocart(this.id);
       
      },
      error: (err) => {
        console.error('Error removing from cart:', err);
        alert('Failed to remove product from cart.');
      }
    });

  }


// remove all 
removeallfromcart(productId: number)
  {
    
    var cartobj ={
      productId : productId,
      userid : this.id
    }

    debugger
    this.cartservice.removeallitem(cartobj).subscribe({
      next: (response) => {
        console.log('Product removed from cart:', response);
      this.toastr.success('Product removed from cart.', 'Success', {
        timeOut: 3000,
        progressBar: true,
        progressAnimation: 'increasing',
        positionClass: 'toast-top-right'
        
      });
        this.gotocart(this.id);
       
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
    debugger
    const dialogRef = this.dialog.open(PaymentComponent, {
      data: { subtotal: this.subtotal }
    });

    debugger
    dialogRef.afterClosed().subscribe((paymentDetails) => {
      debugger
      if (paymentDetails) {
        this.cartservice.paymentcard(paymentDetails).subscribe((isValid) => {
          if (isValid) {
            this.toastr.success('Payment successful', 'Success', {
              timeOut: 3000,
              progressBar: true,
              progressAnimation: 'increasing',
              positionClass: 'toast-top-right'
            })
            this.generateInvoice();

          } else {
            this.toastr.error('Payment failed', 'Error', {
              timeOut: 3000,
              progressBar: true,
              progressAnimation: 'increasing',
              positionClass: 'toast-top-right'
            })
          }
        });
      }
    });
  }


  // generateInvoice()
  // {
  //   debugger
  //   var invoiceRequest = {
  //     userId: this.id,
  //     DeliveryAddress: this.address,
  //     zipcode: this.zipcode,
  //     stateId: this.stateid,
  //     countryId: this.countryid,
  //     Items: this.cartdata.map((item :any) => ({ productId: item.productId , qty: item.qty, sellingPrice: item.sellingPrice ,procuctcode : item.procuctcode}))
  //   }
  //   this.cartservice.getinvoice(invoiceRequest).subscribe({
  //     next: (response) => {
  //       this.invoiceResponse = response; // Store the response in a variable
  //       console.log('Invoice generated successfully:', this.invoiceResponse);
  //       // Optionally, redirect to a new page to display the receipt
  //       this.router.navigate(['/receipt'], { state: { invoiceData: this.invoiceResponse } });
  //     },
  //     error: (err) => {
  //       console.error('Error generating invoice:', err);
  //       alert('Failed to generate invoice.');
  //     }
  //   });

  // }

  generateInvoice()
{
  debugger
  var invoiceRequest = {
    UserId: this.id,
    DeliveryAddress: this.address,
    DeliveryZipcode: this.zipcode,
    DeliveryState: this.stateid,
    DeliveryCountry: this.countryid,
    Items: this.cartdata.map((item :any) => ({ 
      ProductId: item.productId, 
      ProductCode: item.productCode, 
      SaleQty: item.qty, 
      SellingPrice: item.sellingPrice 
    }))
  }
  debugger
  this.cartservice.getinvoice(invoiceRequest).subscribe({
    
    next: (response) => {
      this.invoiceResponse = response;
    
      console.log('Invoice generated successfully:', this.invoiceResponse);

      this.toastr.success('Invoice generated successfully.', 'Success', {
        timeOut: 3000,
        progressBar: true,
        progressAnimation: 'increasing',
        positionClass: 'toast-top-right'    
      })
      sessionStorage.setItem('invoce', JSON.stringify(this.invoiceResponse.data)); 
      sessionStorage.setItem('sales', JSON.stringify(this.invoiceResponse.salesDetails)); 
     debugger
      this.cartdata.forEach((item: any) => {
      this.removeallfromcart(item.productId);
    });
    this.cartdata = [];
    this.calculateSubtotal();
     this.router.navigateByUrl('/invoice')
    },
    error: (err) => {
      console.error('Error generating invoice:', err);
      alert('Failed to generate invoice.');
    }
  });

}

  addToCart(productId:number)
  {
    console.log(productId)
    const cartDetails = {
      userId: this.id, 
      productId: productId,
      qty:  1
    };
debugger
    console.log(cartDetails);

    this.cartservice.Addtocart(cartDetails).subscribe({
      next: (response) => {
        console.log('Product added to cart:', response);
        // alert('Product added to cart successfully!');
        this.toastr.success('Product added to cart.', 'Success', {
          timeOut: 3000,
          progressBar: true,
          progressAnimation: 'increasing',
          positionClass: 'toast-top-right',
          closeButton:false
        });
        this.gotocart(this.id);
        
      },
      error: (err) => {
        console.error('Error adding to cart:', err);
        alert('Failed to add product to cart.');

      }
    });
  }

 
   showInvoiceModal() {
  const modal = document.getElementById("myModal");
  modal!.style.display = "block";
}

closeInvoiceModal() {
  const modal = document.getElementById("myModal");
  // Hide the modal
  modal!.style.display = "none";
}
  

  
} 