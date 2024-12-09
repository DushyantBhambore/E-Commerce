import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { LoginService } from '../../Service/login.service';
import { ToastrService } from 'ngx-toastr';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { MatFormField, MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatNativeDateModule, MatOptionModule } from '@angular/material/core';
import { MatCardModule } from '@angular/material/card';
import { MatDatepickerModule, MatDateRangePicker } from '@angular/material/datepicker';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule,
     CommonModule,MatFormFieldModule,MatIconModule,MatInputModule,
     MatButtonModule,MatSelectModule,MatNativeDateModule,MatOptionModule,
     MatCardModule,MatDatepickerModule,RouterLink
    
    
    ],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  imageFile: File | null = null;
  countries: any;
  states: any;
  roles: any;
  route = inject(ActivatedRoute);
  userObj: any;
  private http = inject(HttpClient);
  private router = inject(Router);
  private service = inject(LoginService);
  private toastr = inject(ToastrService);
  mode!: string;
  userId!: number;
  todayDate=new Date().toISOString().split('T')[0];


  constructor(private fb: FormBuilder) {
    this.registerForm = this.fb.group({
      firstName: ['', [Validators.required,Validators.minLength(3)]],
      lastName: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      mobile: ['', [Validators.required, Validators.pattern('^[0-9]{10}$'),]],
      dob: [new Date().toISOString(), Validators.required],
      roleId: ['', [Validators.required]],
      address: ['', [Validators.required]],
      stateId: ['', [Validators.required]],
      countryId: ['', [Validators.required]],
      zipcode: ['', [Validators.required,Validators.pattern('^[0-9]{6}$')]],
      imageFile: [null, [Validators.required]]
    });
  }


  onInput(event:any){

  }

  ngOnInit(): void {
    this.loadCountries();
    this.loadRoles();
    this.loadStates();
    this.sanitizeField('firstName');
    this.sanitizeField('lastName');

    // this.route.queryParams.subscribe((params) => {
    //   const userId = params['userId'];
    //   if (userId) {
    //     this.fetchUserDetails(userId); // Fetch user data for editing
    //   }
    // });
    this.route.queryParams.subscribe(params => {
      this.mode = params['mode'];
      this.userId = +params['userId']; // Convert userId to a number

      if (this.mode === 'edit'  && this.userId) {
        // Add logic to load user data based on userId if needed
        console.log(`Edit mode: ${this.userId}`);
        this.loadUserData(this.userId);
      }
    });
  }
  
  loadUserData(userId: number) {
    this.service.getbyid(userId).subscribe({
      next: (res :any) => {
        this.userObj = res.data;
        console.log(this.userObj);
        this.registerForm.patchValue({
          firstName: this.userObj.firstName,
          lastName: this.userObj.lastName,
          email: this.userObj.email,
          mobile: this.userObj.mobile,
          dob: this.userObj.dob,
          roleId: this.userObj.roleId,
          address: this.userObj.address,
          stateId: this.userObj.stateId,
          countryId: this.userObj.countryId,
          zipcode: this.userObj.zipcode,
          imageFile: this.userObj.imageFile,
        });
        console.log(res)
      },
      error: (err :any) => {
        console.log(err);
      }
    })
  }



  onFileChange(event: any): void {
    if (event.target.files.length > 0) {
      this.imageFile = event.target.files[0];
      this.registerForm.patchValue({
        imageFile: this.imageFile
      });
    }
  }

  sanitizeField(fieldName: string): void {
    this.registerForm.get(fieldName)?.valueChanges.subscribe((value) => {
      if (value) {
        // Allow trailing spaces, but clean invalid characters and reduce multiple spaces
        const sanitizedValue = value
          .replace(/[^A-Za-z\s]/g, '') // Remove non-letters and non-spaces
          .replace(/\s{2,}/g, ' '); 
          
          // Replace multiple spaces with a single space (excluding trailing)
        if (value !== sanitizedValue) {
          this.registerForm.get(fieldName)?.setValue(sanitizedValue, {
            emitEvent: false, // Avoid recursive calls
          });
        }
      }
    });
  }

  onSubmit(): void {

    
    if(this.registerForm.invalid)
    {
        this.registerForm.markAllAsTouched();
        
      this.toastr.error('Please enter valid data', 'error',
        {
          timeOut: 3000,
          progressBar: true,
          progressAnimation: 'increasing',
          positionClass: 'toast-top-right'

        }
      );
      return;
    }

    const formData = new FormData();
    Object.keys(this.registerForm.controls).forEach(key => {
      const value = this.registerForm.get(key)?.value;
      if (key === 'imageFile' && this.imageFile) {
        formData.append(key, this.imageFile);
      } else if (value) {
        formData.append(key, value);
      }
    });


    this.service.onRegister(formData).subscribe({
      next: (res : any) => {
        if(res.status == 404)
        {
          this.toastr.error("Invalid data", res.message);
        }
        else{
        console.log(res);
        this.toastr.success('User registered successfully! , Username and Passwoed sent your mail');
        this.router.navigate(['/login']);
        }
      },
      error: (err) => {
        console.log(err);
        this.toastr.error('Failed to register user. Please check the data and try again.');
      }
      
    })
    // this.http.post('https://localhost:7295/api/User/RegisterUser', formData).subscribe(
    //   response => {
    //     this.toastr.success('User registered successfully!');
    //     this.router.navigate(['/login']);
    //   },
    //   error => {
    //     console.error('Error registering user', error);
    //     this.toastr.error('Failed to register user. Please check the data and try again.');
    //   }
    // );
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
