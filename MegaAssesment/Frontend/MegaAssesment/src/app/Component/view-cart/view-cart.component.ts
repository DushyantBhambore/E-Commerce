import { Component, inject, OnInit } from '@angular/core';
import { CartService } from '../../Service/cart.service';
import { CommonModule } from '@angular/common';
import { I } from '@angular/cdk/keycodes';

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

  usrname = this.userid.username


  ngOnInit(): void {
    this.gotocart(this.id)
  }
  gotocart(id:number)
  {
    debugger
    console.log(id);
    this.cartservice.getcartbyid(id).subscribe((res:any)=>{
      console.log(res);
      this.cartdata = res.data
      console.log(this.cartdata);
    })

}

cartid! : number
removeFromCart(cartid: number)
  {
    
    var cartobj ={
      cartDetailId : cartid,
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
} 