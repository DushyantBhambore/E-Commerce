import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ForgoTService {

  http = inject(HttpClient)

  forgoturl ='https://localhost:7295/api/User/ForgotPassword'

  constructor() { }

  onforgotpassword(data:any)
  {
    return this.http.post(this.forgoturl,data)
  }
}
