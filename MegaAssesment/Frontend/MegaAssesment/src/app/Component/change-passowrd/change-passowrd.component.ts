import { Component, inject, signal } from '@angular/core';
import { LoginService } from '../../Service/login.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-change-passowrd',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatButtonModule,
    MatInputModule,
    MatFormFieldModule,
    MatIconModule,
  ],
  templateUrl: './change-passowrd.component.html',
  styleUrl: './change-passowrd.component.css'
})
export class ChangePassowrdComponent {

  service = inject(LoginService)

  toastr = inject(ToastrService)
  router = inject(Router)

  
  changePaswordForm = new FormGroup({
    username : new FormControl('', [Validators.required,]),
    newPassword: new FormControl('', [Validators.required,]),
  })

  changePassword() {
    this.service.onChangePassword(this.changePaswordForm.value).subscribe(
      {
        next: (res) => {
          console.log(res);
          this.toastr.success('Password Changed Successfully', 'Success', {
            timeOut: 3000,
            progressBar: true,
            progressAnimation: 'increasing',
            positionClass: 'toast-top-right'
          });
          this.router.navigateByUrl('/login');
        },
        error: (err) => {
          console.log(err);
          this.toastr.error('Password Not Changed', 'Error', {
            timeOut: 3000,
            progressBar: true,
            progressAnimation: 'increasing',
            positionClass: 'toast-top-right'
          });
        }
      })
  }

  hide = signal(true);
  clickEvent(event: MouseEvent) {
    this.hide.set(!this.hide());
    event.stopPropagation();
  }
}
