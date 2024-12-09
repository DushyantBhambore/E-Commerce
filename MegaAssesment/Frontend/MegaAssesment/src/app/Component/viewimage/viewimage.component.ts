import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-viewimage',
  standalone: true,
  imports: [],
  templateUrl: './viewimage.component.html',
  styleUrl: './viewimage.component.css'
})
export class ViewimageComponent  implements OnInit{
 
 
 
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

  viewimg = JSON.parse(sessionStorage.getItem('productlist')!);


  


}
