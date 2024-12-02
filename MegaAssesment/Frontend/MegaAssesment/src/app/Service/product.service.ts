import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  getallprodutsurl = 'https://localhost:7295/api/Product/GetAllProducts'

  http = inject(HttpClient)


  constructor() { }


  getAllProduct()
  {
    return this.http.get(this.getallprodutsurl)
  }
}
