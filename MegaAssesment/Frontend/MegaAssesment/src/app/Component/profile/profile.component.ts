import { Component, inject, model, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../../Service/login.service';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule, JsonPipe } from '@angular/common';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [ReactiveFormsModule,CommonModule,FormsModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit {

  registerForm: FormGroup = new FormGroup({})
  imageFile: File | null = null;
  service = inject(LoginService)
  router = inject(Router)
  private toastr = inject(ToastrService);
  countries: any;
  states: any;
  showModal: boolean = false;
  roles: any;
  userdata :any
  logindatdata = JSON.parse(sessionStorage.getItem('logindata') || '{}');
  todayDate=new Date().toISOString().split('T')[0];


  constructor(private fb: FormBuilder) {
  }
  ngOnInit(): void {
    this.userdata = this.logindatdata
    this.setform()
    this.loadCountries();
    this.loadRoles();
    this.loadStates();
  }
setform()
{
  this.registerForm = this.fb.group({
    userId:[''],
    firstName: ['',],
    lastName: ['',],
    email: ['', ],
    mobile: ['',  Validators.pattern('^[0-9]{10}$')],
    dob: [new Date().toISOString(),],
    roleId: ['', ],
    address: ['', ],
    stateId: ['', ],
    countryId: ['', ],
    zipcode: ['', ],
    imageFile: [null,]

})
}
onKeyPress(event: KeyboardEvent) {
  const charCode = event.which ? event.which : event.keyCode;
  // Allow only numeric characters (keycodes for 0-9)
  if (charCode < 48 || charCode > 57) {
    event.preventDefault(); // Block non-numeric characters
  }
}



onFileChange(event: any): void {
  if (event.target.files.length > 0) {
    this.imageFile = event.target.files[0];
    this.registerForm.patchValue({
      imageFile: this.imageFile
    });
  }
}



  
  onEdit(id:number)
  {debugger
    this.service.getuserbyid(id).subscribe({
      
      next:(res :any)=>{
        console.log(res)
        // this.registerForm.patchValue({
        //   firstName : res.firstName,
        //   lastName : res.lastName,
        //   email:res.email,
        //   mobile : res.mobile,
        //   dob : res.dob,
        //   roleId : res.roleId,
        //   address : res.address,
        //   zipcode: res.zipcode,
        //   countryId: res.countryId,
        //   stateId: res.stateId,
        //   imageFile: res.imageFile

        // })
        this.OpneModal()
        this.setform()
        debugger
        // const { imageFile, ...userData } = res; // Destructure to avoid setting `imageFile`
        this.registerForm.patchValue({
          userId : res.userId,
          firstName : res.firstName,
          lastName : res.lastName,
          email:res.email,
          mobile : res.mobile,
          dob : res.dob,
          roleId : res.roleId,
          address : res.address,
          zipcode: res.zipcode,
          countryId: res.countryId,
          stateId: res.stateId
        }); 

      },
      error:(err)=>{
        console.log(err)
      }
    })

  }

 
  onSubmit(){
    const formData = new FormData();
    const formValues = this.registerForm.getRawValue();
    Object.keys(formValues).forEach(key => {
      const value = formValues[key];
         // If imageFile exists, append it to formData, otherwise append null
    if (key === 'imageFile') {
      if (this.imageFile) {
        formData.append(key, this.imageFile);
      } else {
        formData.append(key, ''); // Add empty string if no image selected
      }
    } else if (value) {
      formData.append(key, value);
    }
  });
      // if (key === 'imageFile' && this.imageFile) {
      //   formData.append(key, this.imageFile);
      // } else if (value) {
      //   formData.append(key, value);
      // } else if (key === 'imageFile' && !this.imageFile) {
      //   formData.append(key, '');
      // }


    debugger
    this.service.onUpdateuser(formData).subscribe(
      {
        
        next:(res: any)=>{
          console.log(res);
          this.toastr.success("Profile Updted Successfully",'sucess',{
            
            timeOut: 3000,
            progressBar: true,
            progressAnimation: 'increasing',
            positionClass: 'toast-top-right'
          
          })
          console.log(res.data);
          sessionStorage.setItem("logindata", JSON.stringify(res.data));
          this.CloseModal();
          window.location.reload();

        },
        error: (err) => {
          console.log(err);
          this.toastr.error('Failed to register user. Please check the data and try again.');
        }
        

      })

  }
    // Modal open handler
   
   

  OpneModal(){
    debugger
    const modal = document.getElementById("myModal");
    if (modal) {
      modal.style.display = "block";
    }


  }

  // modal close
  CloseModal()
  {
    this.registerForm.reset();
    const modal = document.getElementById("myModal");
    if (modal) {
      modal.style.display = "none";
    }
  }

  private loadCountries(): void {
    this.service.getAllCountry().subscribe({
      next: (res) => {
        console.log('Countries loaded:', res);
        this.countries = res;
      },
      error: (error) => {
        console.error('Error loading countries', error);
      }
    });
  }

  private loadRoles(): void {
    this.service.getAllRole().subscribe({
      next: (res) => {
        console.log('Roles loaded:', res);
        this.roles = res;
      },
      error: (error) => {
        console.error('Error loading roles', error);
      }
    });
  }

  private loadStates(): void {
    this.service.getAllState().subscribe({
      next: (res) => {
        console.log('States loaded:', res);
        this.states = res;
      },
      error: (error) => {
        console.error('Error loading states', error);
      }
    });
  }




}
