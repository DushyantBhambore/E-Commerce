import { Component, inject } from '@angular/core';
import { ProductService } from '../../Service/product.service';

@Component({
  selector: 'app-product',
  standalone: true,
  imports: [],
  templateUrl: './product.component.html',
  styleUrl: './product.component.css'
})
export class ProductComponent {


  

  service = inject(ProductService)


}
