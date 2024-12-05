import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  loginurl = 'https://localhost:7295/api/User/LoginUser'
  
  registerurl = 'https://localhost:7295/api/User/RegisterUser' 

  getallcountryurl ='https://localhost:7295/api/Country/GetAllCountry'
  

  getstatebyidurl = 'https://localhost:7295/api/State'
  
  getallstateurl ='https://localhost:7295/api/State/GetAllState'

  getallroleurl = 'https://localhost:7295/api/Role/GetAllRoles'

  verifyotpurl = 'https://localhost:7295/api/User/VerifyOtp'

  getbyidurl = 'https://localhost:7295/api/User/GetUserById'

  changepasswordurl = 'https://localhost:7295/api/User/ChangePassword'

  forgotpasswordurl = 'https://localhost:7295/api/User/ForgotPassword'

  http = inject(HttpClient)

  constructor() { }

  onLogin(data: any) {
    return this.http.post(this.loginurl, data)
  }
  onVerifyOtp(data: any) {
    return this.http.post(this.verifyotpurl, data)
  }

  onRegister(data: any) {
    return this.http.post(this.registerurl, data)
  }

  getAllCountry() {
    return this.http.get(this.getallcountryurl)
  }

  getAllState(){
    return this.http.get(this.getallstateurl)
  }

  getAllRole()
  {
    return this.http.get(this.getallroleurl)
  }

  getstatebyid(id:number)
  {
    return this.http.get(`${this.getstatebyidurl}/${id}`)
  }


  getuserbyid(id:number)
  {
    return this.http.get(`https://localhost:7295/api/State/GetStateById/${id}`)
  }

  getbyid(id:number)
  {
    return this.http.get(`${this.getbyidurl}/${id}`)
  }


  onforgotpassword(data:any)
  {
    return this.http.post(this.forgotpasswordurl,data)
  }

  onChangePassword(data:any)
  {
    return this.http.put(this.changepasswordurl,data)
  }


}
