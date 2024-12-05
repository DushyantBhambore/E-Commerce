import { Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../../Service/login.service';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit {



  service = inject(LoginService)
  router = inject(Router)
  userdata :any
  logindatdata = JSON.parse(sessionStorage.getItem('logindata') || '{}');
  
 
  ngOnInit(): void {
    this.userdata = this.logindatdata
  }

  onEdit(id:number)
  {
    this.service.getbyid(id).subscribe({
      next:(res)=>{
        console.log(res)
      },
      error:(err)=>{
        console.log(err)
      }
    })


  }






}
