import { Component, inject } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { LoginService } from '../../Service/login.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-otp',
  standalone: true,
  imports: [],
  templateUrl: './otp.component.html',
  styleUrl: './otp.component.css'
})
export class OTPComponent {

  verifytform: FormGroup = new FormGroup({});
  username: string = '';
  service = inject(LoginService);
  router = inject(Router);
  toastr = inject(ToastrService);

  

  // setOtpForm() {
  //   this.username = usernamedata.username; // Save the username for OTP verification
  //   this.verifytform = this.fb.group({
  //     username: [username],
  //     otp: ['']
  //   });
  }

  // onOTP() {
  //   this.toastr.info('Verifying OTP...', 'Please wait', {
  //     timeOut: 2000,
  //     progressBar: true,
  //     progressAnimation: 'increasing',
  //     positionClass: 'toast-top-right',
  //   });
  
  //   this.service.onVerifyOtp(this.verifytform.value).subscribe({
  //     next: (res: any) => {
  //       localStorage.setItem('token', res.token);
  //       localStorage.setItem('profileimage', res.data.imageFile);
  //       sessionStorage.setItem('logindata', JSON.stringify(res.data));
  //       sessionStorage.setItem('role',(res.data.roleId))
  //       this.router.navigateByUrl('/dashboard');
  //       this.toastr.success('Login successful', 'Success', {
  //         timeOut: 3000,
  //         progressBar: true,
  //         progressAnimation: 'increasing',
  //         positionClass: 'toast-top-right'
  //       });
  //     },
  //     error: (err) => {
  //       this.toastr.error('OTP verification failed', 'Error', {
  //         timeOut: 3000,
  //         progressBar: true,
  //         progressAnimation: 'increasing',
  //         positionClass: 'toast-top-right'
  //       });
  //     }
  //   });
  // }
  



