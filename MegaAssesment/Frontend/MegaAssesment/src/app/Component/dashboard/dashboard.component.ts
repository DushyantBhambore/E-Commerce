import { Component, inject, OnInit } from '@angular/core';
import { ProductService } from '../../Service/product.service';
import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [MatButtonModule,MatCardModule,CommonModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit {
  
  productlist : any =[]
  service = inject(ProductService)
  
  ngOnInit(): void {
    this.getProduct();
  }


  getProduct()
  {
    this.service.getAllProduct().subscribe(
      {
        next:(res)=>
        {
          console.log(res);
          this.productlist = res;
        },
        error:(err)=>
        {
          console.log(err);
        }
        
      }
    )
  }


  ViewImgae()
  {
    
  }


  





}
