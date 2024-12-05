  import { HttpClient } from '@angular/common/http';
  import { inject, Injectable } from '@angular/core';
  import { BehaviorSubject, tap } from 'rxjs';

  @Injectable({
    providedIn: 'root'
  })
  export class CartService {


    addtocarturl = 'https://localhost:7295/api/Cart/AddToCart'

    removecarturl = 'https://localhost:7295/api/Cart/DeleteFromCart'

    getcartbyidurl = 'https://localhost:7295/api/Cart/GetCartById'

    updatecarturl = 'https://localhost:7295/api/Cart/UpdateCart'

    paymentcardurl ='https://localhost:7295/api/Cart/PayementCard'

    getaddressurl ='https://localhost:7295/api/Cart/AddAddess'

    postinvoiceurl = 'https://localhost:7295/api/Sales/place-order'


    http = inject(HttpClient)


    public cartCount$ = new BehaviorSubject<number>(0);


    constructor() { }
    Addtocart(data: any) {
      return this.http.post(this.addtocarturl, data).pipe(
        tap(() => {
          // Increment the count after a successful API call
          const currentCount = this.cartCount$.value;
          this.cartCount$.next(currentCount + 1);
        })
      );
    }
    
  removeFromCart(obj: any) {
    return this.http.delete(this.removecarturl, { body: obj }).pipe(
      tap(() => {
        // Decrement the count after a successful API call
        const currentCount = this.cartCount$.value;
        this.cartCount$.next(currentCount > 0 ? currentCount - 1 : 0);
      })
    );
  }
    
  getCartCount() {
    return this.cartCount$.asObservable();
  }

    getcartbyid(id : number)
    {
      return this.http.get(`${this.getcartbyidurl}/${id}`)
    }

    updateCart(data : any)
    {
      return this.http.put(this.updatecarturl,data)
    }


    paymentcard(data : any)
    {
      return this.http.post(this.paymentcardurl,data)
    }

    getUserProfile(obj :any)
    {
      return this.http.post(this.getaddressurl,obj)
    }

    getinvoice(data : any)
    {
      return this.http.post(this.postinvoiceurl,data)
    }

  }
