import { Component, inject, OnInit } from '@angular/core';
import { ProductService } from '../../Service/product.service';
import { Router } from '@angular/router';
import { Form, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule, DatePipe } from '@angular/common';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-product',
  standalone: true,
  imports: [ReactiveFormsModule,CommonModule,DatePipe],
  templateUrl: './product.component.html',
  styleUrl: './product.component.css'
})
export class ProductComponent implements OnInit {


  productlist : any =[]
  
  service = inject(ProductService)
  route = inject(Router)
  toastr = inject(ToastrService)


  ngOnInit(): void {
    this.getProduct();
  }

  constructor(private fb: FormBuilder) {
    this.productform = this.fb.group({
      prodcutId: ['', [Validators.required]],
      productName: ['', [Validators.required]],
      productImage: [null, [Validators.required,]],
      category: ['', [Validators.required, ]],
      purchaseDate: [new Date().toISOString(), Validators.required],
      sellingPrice: ['', [Validators.required]],
      purchasePrice: ['', [Validators.required]],
      brand: ['', [Validators.required]],
      stock: ['', [Validators.required]],
    
    });
  }
  productImage: File | null = null;
  productform : FormGroup = new FormGroup({})

  // modal open 
  OpneModal(){
    const modal = document.getElementById("myModal");
    if (modal) {
      modal.style.display = "block";
    }
  }

  // modal close
  CloseModal()
  {
    const modal = document.getElementById("myModal");
    if (modal) {
      modal.style.display = "none";
    }
  }

  // image upload
  onFileChange(event: any): void {
    if (event.target.files.length > 0) {
      this.productImage = event.target.files[0];
      this.productform.patchValue({
        productImage: this.productImage
      });
    }
  }
    onSubmit()
    {
      debugger
      const formData = new FormData();
      Object.keys(this.productform.controls).forEach(key => {
        const value = this.productform.get(key)?.value;
        if (key === 'productImage' && this.productImage) {
          formData.append(key, this.productImage);
        } else if (value) {
          formData.append(key, value);
        }
      });

      // update product 
      if(this.productform.value.prodcutId != 0)
      {
        debugger

        this.service.updateProduct(formData).subscribe({
          next: (res:any) => {
            console.log(res.data);
            this.toastr.success('Product Updated successfully!');
            this.CloseModal();
            this.productform.reset()
            this.getProduct();
            const updatedProduct = res; // Assuming the service returns the updated product
        const index = this.productlist.findIndex((p:any) => p.productId === updatedProduct.productId);
        if (index !== -1) {
          this.productlist[index] = updatedProduct; // Replace the old product with the updated one
        }
          },
          error: (err) => {
            console.log(err);
            this.toastr.error('Failed to update product.');
          }
        })
      }
      else
      {
        this.service.addProduct(formData).subscribe({
          next: (res) => {
            console.log(res);
            this.toastr.success('Product Added successfully!');
            this.CloseModal();
            this.productform.reset();
            this.getProduct();
            this.productlist.push(res); // Assuming the service returns the added product


          },
          error: (err) => {
            console.log(err);
            this.toastr.error('Failed to add product.');
          }
          
        })
      }
     
    }



    // getAll Product 

    getProduct()
    {
      this.service.getAllProduct().subscribe({
        next:(res)=>
        {
          console.log(res);
          this.productlist = res;
        },
        error:(err)=>
        {
          console.log(err);
        }
      })
      
    }

    editProduct(item:any)
    {
      debugger
      console.log(item);
      this.OpneModal();
      // this.productform.patchValue(item);
      this.productform.patchValue({
        productId: item.productId,
        productName: item.productName,
        category: item.category,
        purchaseDate: item.purchaseDate,
        sellingPrice: item.sellingPrice,
        purchasePrice: item.purchasePrice,
        brand: item.brand,
        stock: item.stock,
      });
    }

    deleteProduct(id:number)
    {
      const data = confirm('Are you sure do you want delete this Id ');
      if (data == true) {
        this.service.deleteProduct(id).subscribe({
          next: (res) => {
            console.log('deleted', res);
            alert('delete successfully ');
          },
          error: (error) => {
            alert('I am error in delete');
            console.log('I am error in delete',error);
          },
        });
      }

    }

  }

