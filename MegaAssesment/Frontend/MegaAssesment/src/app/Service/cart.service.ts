import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CartService {


  addtocarturl = 'https://localhost:7295/api/Cart/AddToCart'

  removecarturl = 'https://localhost:7295/api/Cart/DeleteFromCart'

  getcartbyidurl = 'https://localhost:7295/api/Cart/GetCartById'


  http = inject(HttpClient)


  public cartCount$ = new BehaviorSubject<number>(0);


  constructor() { }
  Addtocart(data:any)
  {
    return this.http.post(this.addtocarturl,data)
  }
  removeFromCart(data:any)
  {
    return this.http.delete(this.removecarturl,data)
  }
  getCartCount() {
    return this.cartCount$.asObservable();
  }

  getcartbyid(id : number)
  {
    return this.http.get(`${this.getcartbyidurl}/${id}`)
  }
}
