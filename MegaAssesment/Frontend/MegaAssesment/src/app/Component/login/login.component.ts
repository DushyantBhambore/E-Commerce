import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { LoginService } from '../../Service/login.service';
import { Router, RouterLink } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatButtonModule,
    MatInputModule,
    MatFormFieldModule,
    MatIconModule,
    RouterLink
  ],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoginComponent implements OnInit {

  isLoginSuccessful = false;
     loginform: FormGroup = new FormGroup({});
  verifytform: FormGroup = new FormGroup({});
  username: string = '';
  service = inject(LoginService);
  router = inject(Router);
  toastr = inject(ToastrService);
  isLoading = signal(false); // To show a loading state

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {
    this.setForm();

  }

  setForm() {
    this.loginform = this.fb.group({
      username: [''],
      password: [''],
    });
  }

  setOtpForm() {
    const username = this.loginform.get('username')?.value;
    this.username = username; // Save the username for OTP verification
    this.verifytform = this.fb.group({
      username: [username],
      otp: ['']
    });
  }

  

  // onLogin() {
  //   this.service.onLogin(this.loginform.value).subscribe({
  //     next: (res) => {
  //       console.log(res);
  //       this.setOtpForm();
  //       this.isLoginSuccessful = true; 
  //       this.toastr.success('OTP sent successfully', 'Success', {
  //         timeOut: 3000,
  //         progressBar: true,
  //         progressAnimation: 'increasing',
  //         positionClass: 'toast-top-right',
  //       });
  //     },
  //     error: (err) => {
  //       console.log(err);
  //       this.toastr.error('Login failed', 'Error', {
  //         timeOut: 3000,
  //         progressBar: true,
  //         progressAnimation: 'increasing',
  //         positionClass: 'toast-top-right'
  //       });
  //     }
  //   });
  // }

  // onOTP() {
  //   this.service.onVerifyOtp(this.verifytform.value).subscribe({
  //     next: (res:any) => {
  //       console.log(res);
  //       localStorage.setItem('token', res.token);
  //       localStorage.setItem('profileimage ',res.data.imageFile);
  //       this.router.navigateByUrl('/dashboard');
  //       this.toastr.success('Login successful', 'Success', {
  //         timeOut: 3000,
  //         progressBar: true,
  //         progressAnimation: 'increasing',
  //         positionClass: 'toast-top-right'
  //       });
  //     },
  //     error: (err) => {
  //       console.log(err);
  //       this.toastr.error('OTP verification failed', 'Error', {
  //         timeOut: 3000,
  //         progressBar: true,
  //         progressAnimation: 'increasing',
  //         positionClass: 'toast-top-right'
  //       });
  //     }
  //   });
  // }


  onLogin() {
    this.toastr.info('Logging in...', 'Please wait', {
      timeOut: 2000,
      progressBar: true,
      progressAnimation: 'increasing',
      positionClass: 'toast-top-right',
    });
  
    this.service.onLogin(this.loginform.value).subscribe({
      next: (res) => {
        this.toastr.success('OTP sent successfully', 'Success', {
          timeOut: 3000,
          progressBar: true,
          progressAnimation: 'increasing',
          positionClass: 'toast-top-right',
        });
        this.isLoginSuccessful = true;
        this.setOtpForm();

      },
      error: (err) => {
        this.toastr.error('Login failed', 'Error', {
          timeOut: 3000,
          progressBar: true,
          progressAnimation: 'increasing',
          positionClass: 'toast-top-right'
        });
      }
    });
  }
  
  onOTP() {
    this.toastr.info('Verifying OTP...', 'Please wait', {
      timeOut: 2000,
      progressBar: true,
      progressAnimation: 'increasing',
      positionClass: 'toast-top-right',
    });
  
    this.service.onVerifyOtp(this.verifytform.value).subscribe({
      next: (res: any) => {
        localStorage.setItem('token', res.token);
        localStorage.setItem('profileimage', res.data.imageFile);
        sessionStorage.setItem('logindata', JSON.stringify(res.data));
        sessionStorage.setItem('role',(res.data.roleId))
        this.router.navigateByUrl('/dashboard');
        this.toastr.success('Login successful', 'Success', {
          timeOut: 3000,
          progressBar: true,
          progressAnimation: 'increasing',
          positionClass: 'toast-top-right'
        });
      },
      error: (err) => {
        this.toastr.error('OTP verification failed', 'Error', {
          timeOut: 3000,
          progressBar: true,
          progressAnimation: 'increasing',
          positionClass: 'toast-top-right'
        });
      }
    });
  }
  


  hide = signal(true);
  clickEvent(event: MouseEvent) {
    this.hide.set(!this.hide());
    event.stopPropagation();
  }
}
