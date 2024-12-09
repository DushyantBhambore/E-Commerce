import { Component, inject, OnInit } from '@angular/core';
import { ProductService } from '../../Service/product.service';
import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
import { CommonModule, JsonPipe } from '@angular/common';
import { CartService } from '../../Service/cart.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [MatButtonModule,MatCardModule,CommonModule,],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit {
  
  productlist : any =[]
  service = inject(ProductService)
  cartservice = inject(CartService)
  userdata  = JSON.parse(sessionStorage.getItem('logindata') || '{}')
  userId = this.userdata.userId
  toastr=inject(ToastrService)
  
  
  ngOnInit(): void {
    this.getProduct();
  }


  getProduct()
  {
    this.service.getAllProduct().subscribe(
      {
        next:(res)=>
        {
          console.log(res);
          this.productlist = res;
          sessionStorage.setItem('productlist', JSON.stringify(res));
        },
        error:(err)=>
        {
          console.log(err);
        }
        
      }
    )
  }

  cartCount = 0;
  cartItems = [];
  
  addToCart(item:number)
  {
    console.log(item)
    const cartDetails = {
      userId: this.userId, 
      productId: item,
      qty:  1
    };

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
        this.getProduct();
        
      },
      error: (err) => {
        console.error('Error adding to cart:', err);
        alert('Failed to add product to cart.');

      }
    });
  }


  removeFromCart(cartDetailId: number)
  {
    this.cartservice.removeFromCart(cartDetailId).subscribe({
      next: (response) => {
        console.log('Product removed from cart:', response);
        this.toastr.success('Product removed from cart.', 'Success', {
          timeOut: 3000,
          progressBar: true,
          progressAnimation: 'increasing',
          positionClass: 'toast-top-right'
          
        });
        alert('Product removed from cart successfully!');
        this.getProduct(); // Refresh the cart items after removing
    
      },
      error: (err) => {
        console.error('Error removing from cart:', err);
        alert('Failed to remove product from cart.');
      }
    });

  }
    // get cart 
    ischeck = false
    cartdata :any

   
      
      
  gotocart(userId:number)
    {
      debugger
      console.log(userId);
      this.cartservice.getcartbyid(userId).subscribe((res:any)=>{
        console.log(res);
        this.cartdata = res.data
        console.log(this.cartdata);
      })
      
    }
    




}

