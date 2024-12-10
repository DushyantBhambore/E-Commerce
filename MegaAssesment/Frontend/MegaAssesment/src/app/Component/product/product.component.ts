import Swal from 'sweetalert2';
import { Component, inject, OnInit } from '@angular/core';
import { ProductService } from '../../Service/product.service';
import { Router } from '@angular/router';
import { AbstractControl, Form, FormBuilder, FormControl, FormGroup, ReactiveFormsModule, ValidationErrors, Validators } from '@angular/forms';
import { CommonModule, DatePipe } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { sellingPriceGreaterThanPurchasePrice } from '../CustomeValidaors/sellingPriceGreaterThanPurchasePrice';

@Component({
  selector: 'app-product',
  standalone: true,
  imports: [ReactiveFormsModule,CommonModule,
    DatePipe,
  ],
  templateUrl: './product.component.html',
  styleUrl: './product.component.css'
})
export class ProductComponent implements OnInit {


  productlist : any =[]
  
  service = inject(ProductService)
  route = inject(Router)
  toastr = inject(ToastrService)
  todayDate=new Date().toISOString().split('T')[0];





  constructor(private fb: FormBuilder) {
    this.productform = this.fb.group({
      prodcutId: ['', [Validators.required,]],
    productName: ['', [Validators.required, Validators.pattern(/^[A-Za-z]+(?: [A-Za-z]+)*\s*$/)]],
      productImage: [null, [Validators.required,]],
      category: ['', [Validators.required, ]],
      purchaseDate: [new Date().toISOString(), Validators.required],
      sellingPrice: ['', [Validators.required]],
      purchasePrice: ['', [Validators.required]],
      brand: ['', [Validators.required]],
      stock: ['', [Validators.required]],
    
    },{validators: sellingPriceGreaterThanPurchasePrice } );
  }
  productImage: File | null = null;
  productform : FormGroup = new FormGroup({})



  ngOnInit(): void {
    this.getProduct();
    this.sanitizeField('productName');
    this.sanitizeField('category');
    this.sanitizeField('brand');

  }

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
    this.productform.reset();
    const modal = document.getElementById("myModal");
    if (modal) {
      modal.style.display = "none";
    }
  }

  sanitizeField(fieldName: string): void {
    this.productform.get(fieldName)?.valueChanges.subscribe((value) => {
      if (value) {
        // Allow trailing spaces, but clean invalid characters and reduce multiple spaces
        const sanitizedValue = value
          .replace(/[^A-Za-z\s]/g, '') // Remove non-letters and non-spaces
          .replace(/\s{2,}/g, ' '); 
          
          // Replace multiple spaces with a single space (excluding trailing)
        if (value !== sanitizedValue) {
          this.productform.get(fieldName)?.setValue(sanitizedValue, {
            emitEvent: false, // Avoid recursive calls
          });
        }
      }
    });
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

  onSubmit() {
    const formData = new FormData();
    const formValues = this.productform.getRawValue();
    Object.keys(formValues).forEach(key => {
      const value = formValues[key];
      if (key === 'productImage' && this.productImage) {
        formData.append(key, this.productImage);
      } else if (value) {
        formData.append(key, value);
      } else if (key === 'productImage' && !this.productImage) {
        formData.append(key, '');
      }
    });
  
    // update product
    if (formValues.prodcutId > 0) {
      debugger;
  
      this.service.updateProduct(formData).subscribe({
        next: (res: any) => {
          console.log(res.data);
          this.toastr.success('Product Updated successfully!');
          this.CloseModal();
          this.getProduct();
          const updatedProduct = res; // Assuming the service returns the updated product
          const index = this.productlist.findIndex((p: any) => p.productId === updatedProduct.productId);
          if (index !== -1) {
            this.productlist[index] = updatedProduct; // Replace the old product with the updated one
          }
        },
        error: (err) => {
          console.log(err);
          this.toastr.error('Failed to update product.');
        }
      });
    } else {
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
      });
    }
  }
  //   onSubmit()
  //   {
  //     debugger
  //     if(this.productform.value.prodcutId >0)
  //     {
  //       this.service.updateProduct(this.productform).subscribe({
  //         next: (res:any) => {
  //           console.log(res.data);
  //           this.toastr.success('Product Updated successfully!');
  //           this.CloseModal();
  //           this.productform.reset()
  //           this.getProduct();
  //           const updatedProduct = res; // Assuming the service returns the updated product
  //       const index = this.productlist.findIndex((p:any) => p.productId === updatedProduct.productId);
  //       if (index !== -1) {
  //         this.productlist[index] = updatedProduct; // Replace the old product with the updated one
  //       }
  //         },
  //         error: (err) => {
  //           console.log(err);
  //           this.toastr.error('Failed to update product.');
  //         }
  //       })
        
  //     }
  //     else
  //     {

      
  //     const formData = new FormData();
  // Object.keys(this.productform.controls).forEach(key => {
  //   const value = this.productform.get(key)?.value;
  //   if (key === 'productImage' && this.productImage) {
  //     formData.append(key, this.productImage);
  //   } else if (value) {
  //     formData.append(key, value);
  //   } else if (key === 'productImage' && !this.productImage) {
  //     formData.append(key, '');
  //   }
  // });
  // debugger
  //     // update product 
     
      
  //       this.service.addProduct(formData).subscribe({
  //         next: (res) => {
  //           console.log(res);
  //           this.toastr.success('Product Added successfully!');
  //           this.CloseModal();
  //           this.productform.reset();
  //           this.getProduct();
  //           this.productlist.push(res); // Assuming the service returns the added product


  //         },
  //         error: (err) => {
  //           console.log(err);
  //           this.toastr.error('Failed to add product.');
  //         }
          
  //       })
  //     }
     
  //   }



    // getAll Product 

    getProduct()
    {
      this.service.getAllProduct().subscribe({
        next:(res)=>
        {
          console.log(res);
          this.productlist = res;
          console.log(this.productlist);
        },
        error:(err)=>
        {
          console.log(err);
        }
      })
      
    }

    editProduct(id:number)
    {
      // debugger
      // console.log(item);
      // this.OpneModal();
      // // this.productform.patchValue(item);
      // this.productform.patchValue({
      //   productId: item.productId,
      //   productName: item.productName,
      //   category: item.category,
      //   purchaseDate: item.purchaseDate,
      //   sellingPrice: item.sellingPrice,
      //   purchasePrice: item.purchasePrice,
      //   brand: item.brand,
      //   stock: item.stock,
      // });

      this.service.getproductbyid(id).subscribe({
        next:(res :any)=>
        {
          debugger
          this.OpneModal();
          console.log(res);
          this.productform.patchValue({
            prodcutId: res.prodcutId,
            productName: res.productName,
            category: res.category,
            purchaseDate: res.purchaseDate,
            sellingPrice: res.sellingPrice,
            purchasePrice: res.purchasePrice,
            brand: res.brand,
            stock: res.stock,
            productImage: res.productImage
          });
          
        },
        error:(err)=>
        {
          console.log(err);
        }
      })
    }


    deleteProduct(id: number) {
      // Using SweetAlert2 for confirmation
      Swal.fire({
        title: 'Are you sure?',
        text: `Do you want to delete the product with ID ${id}?`,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: 'Cancel',
        reverseButtons: true
      }).then((result : any) => {
        if (result.isConfirmed) {
          this.service.deleteProduct(id).subscribe({
            next: (res : any) => {
              console.log('Deleted', res);
              this.getProduct();
              this.toastr.success('Product Deleted successfully', 'Success', {
                timeOut: 3000,
                progressBar: true,
                progressAnimation: 'increasing',
                positionClass: 'toast-top-right'
              });
            },
            error: (error) => {
              console.error('Error in delete:', error);
              this.toastr.error('Failed to delete product. Please try again.', 'Error', {
                timeOut: 3000,
                progressBar: true,
                progressAnimation: 'increasing',
                positionClass: 'toast-top-right'
              });
            }
          });
        } else {
          // If user cancels, do nothing
          console.log('Product deletion canceled');
        }
      });
    }
    


    // deleteProduct(id:number)
    // {

      
    //   const data = confirm('Are you sure do you want delete this Id ');
    //   if (data == true) {
    //     this.service.deleteProduct(id).subscribe({
    //       next: (res) => {
    //         console.log('deleted', res);
    //         this.toastr.error('Product Deleted successfully', 'Error', {
    //           timeOut: 3000,
    //           progressBar: true,
    //           progressAnimation: 'increasing',
    //           positionClass: 'toast-top-right'
    //         })
    //       },
    //       error: (error) => {
    //         alert('I am error in delete');
    //         console.log('I am error in delete',error);
    //       },
    //     });
    //   }

    // }



   

  }

