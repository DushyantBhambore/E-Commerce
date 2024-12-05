import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  getallprodutsurl = 'https://localhost:7295/api/Product/GetAllProduct'

  addproducturl = 'https://localhost:7295/api/Product/AddProduct'

  updateproducturl = 'https://localhost:7295/api/Product/UpdateProduct'

  deleteproducturl = 'https://localhost:7295/api/Product/DeleteProduct'

  getproductbyidurl ='https://localhost:7295/api/Product/GetProductByid'

  http = inject(HttpClient)


  constructor() { }


  getAllProduct()
  {
    return this.http.get(this.getallprodutsurl)
  }

  addProduct(data:any)
  {
    return this.http.post(this.addproducturl,data)
  }

  updateProduct(data:any)
  {
    return this.http.put(this.updateproducturl,data)
  }

  deleteProduct(id:number)
  {
    return this.http.delete(this.deleteproducturl+"/"+id)
  }

  getproductbyid(id:number)
  {
    return this.http.get(this.getproductbyidurl+"/"+id)
  }
}
