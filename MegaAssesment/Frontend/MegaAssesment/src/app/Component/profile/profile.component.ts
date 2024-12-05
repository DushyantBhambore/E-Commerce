import { Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit {



  router = inject(Router)
  userdata :any
  logindatdata = JSON.parse(sessionStorage.getItem('logindata') || '{}');

  


  onEdit(id: number) {
    this.router.navigate(['/register'], { queryParams: { userId: id } });
  }
  ngOnInit(): void {
    this.userdata = this.logindatdata
  }




}
