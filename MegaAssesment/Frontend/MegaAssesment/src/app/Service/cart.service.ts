import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CartService {


  addtocarturl = 'https://localhost:7295/api/Cart/AddToCart'


  http = inject(HttpClient)

  constructor() { }


  Addtocart(data:any)
  {
    return this.http.post(this.addtocarturl,data)
  }
}
