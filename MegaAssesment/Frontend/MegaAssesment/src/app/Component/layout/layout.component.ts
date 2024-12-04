import { Component, inject } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { CartService } from '../../Service/cart.service';

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [RouterOutlet,RouterLink],
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.css'
})
export class LayoutComponent {

  router = inject(Router)

  service = inject(CartService)
  cartCount!: number;

constructor() {
  this.service.getCartCount().subscribe((count) => {
    this.cartCount = count;
  });
}

  userid = JSON.parse(sessionStorage.getItem('logindata') || '{}');
  id : number = this.userid.userId

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
    this.service.getcartbyid(id).subscribe((res:any)=>{
      console.log(res);
      this.data = res.data
      console.log(this.data);
    })
  }

}
