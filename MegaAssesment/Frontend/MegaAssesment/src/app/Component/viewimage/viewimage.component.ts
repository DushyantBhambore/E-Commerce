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
    this.viewProductImage();
  }

  viewimg = JSON.parse(sessionStorage.getItem('productlist')!);


  image = this.viewimg.productImage


  viewProductImage() {
    console.log(this.viewimg);
  }


  


}
