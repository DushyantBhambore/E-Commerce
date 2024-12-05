import { Component, inject } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { CartService } from '../../Service/cart.service';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatIconTestingModule } from '@angular/material/icon/testing';
import { MatMenuModule } from '@angular/material/menu';

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [RouterOutlet,RouterLink,CommonModule,MatIconTestingModule,MatIconModule,MatMenuModule],
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.css'
})
export class LayoutComponent {

  router = inject(Router)

  cartCount!: number;

  

  userid = JSON.parse(sessionStorage.getItem('logindata') || '{}');
  id : number = this.userid.userId


  constructor(private cartService: CartService,) {
    this.cartService.getCartCount().subscribe((count) => {
      this.cartCount = count;
    });
  }
  dropdownOpen = false;
  profile = localStorage.getItem('profileimage');
  onLogOut(){
    localStorage.removeItem('token');
    this.router.navigateByUrl('login');
  }
  toggleDropdown() { this.dropdownOpen = !this.dropdownOpen; }


  data :any

  gotocart(id:number)
  {
    debugger
    console.log(id);
    this.cartService.getcartbyid(id).subscribe((res:any)=>{
      console.log(res);
      this.data = res.data
      console.log(this.data);
    })
  }

}
