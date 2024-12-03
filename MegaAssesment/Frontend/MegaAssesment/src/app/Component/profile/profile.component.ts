import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit {



  userdata :any
  logindatdata = JSON.parse(sessionStorage.getItem('logindata') || '{}');


  ngOnInit(): void {
    this.userdata = this.logindatdata
  }




}
