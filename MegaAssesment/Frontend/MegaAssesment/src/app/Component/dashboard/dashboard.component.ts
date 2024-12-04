import { Component, inject, OnInit } from '@angular/core';
import { ProductService } from '../../Service/product.service';
import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
import { CommonModule } from '@angular/common';
import { CartService } from '../../Service/cart.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [MatButtonModule,MatCardModule,CommonModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit {
  
  productlist : any =[]
  service = inject(ProductService)
  cartservice = inject(CartService)
  userdata  = JSON.parse(sessionStorage.getItem('logindata') || '{}')
  userId = this.userdata.userId

  
  
  ngOnInit(): void {
    this.getProduct();
    this.cartservice.getCartCount().subscribe((count) => {
      this.cartCount = count;
    });
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
      userId: this.userId, // Replace with actual user ID logic
      productId: item,
      qty:  1// Adjust the quantity as needed
    };

    console.log(cartDetails);

    this.cartservice.Addtocart(cartDetails).subscribe({
      next: (response) => {
        console.log('Product added to cart:', response);
        alert('Product added to cart successfully!');
        this.cartCount++;
        
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
        alert('Product removed from cart successfully!');
        this.getProduct(); // Refresh the cart items after removing
        this.cartCount--;
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

