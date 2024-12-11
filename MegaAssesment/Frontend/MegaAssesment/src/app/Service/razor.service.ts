import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class RazorService {


  createorderurl ='https://localhost:7295/api/Payments/create-order'

  verifypaymenturl = 'https://localhost:7295/api/Payments/verify-payment'

  http = inject(HttpClient);

  constructor() { }

  createOrder(amount: number) {
    return this.http.post(this.createorderurl, { amount });
  }

  verifyPayment(paymentData: any) {
    return this.http.post(this.verifypaymenturl, paymentData);
  }



}
