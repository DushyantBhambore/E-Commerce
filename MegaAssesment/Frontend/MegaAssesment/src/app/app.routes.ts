import { Component } from '@angular/core';
import { Routes } from '@angular/router';
import { LoginComponent } from './Component/login/login.component';
import { RegisterComponent } from './Component/register/register.component';
import { LayoutComponent } from './Component/layout/layout.component';
import { ProfileComponent } from './Component/profile/profile.component';
import { ChangePassowrdComponent } from './Component/change-passowrd/change-passowrd.component';
import { DashboardComponent } from './Component/dashboard/dashboard.component';
import { ProductComponent } from './Component/product/product.component';
import { ViewCartComponent } from './Component/view-cart/view-cart.component';
import { PaymentComponent } from './Component/payment/payment.component';
import { InvoiceComponent } from './Component/invoice/invoice.component';
import { ForgotPasswordComponent } from './Component/forgot-password/forgot-password.component';
import { authGuard } from './Guard/auth.guard';
import { ViewimageComponent } from './Component/viewimage/viewimage.component';

export const routes: Routes = [
    {
        path: '',
        redirectTo: 'login',
        pathMatch : 'full'
    },
    {
        path: 'login',
        component: LoginComponent
    },
    {
        path:'',
        component:LayoutComponent,
        children:[
            {

                path:'dashboard',
                component:DashboardComponent,
                canActivate:[authGuard]
            },
            {
                path:'product',
                component:ProductComponent,
                canActivate: [authGuard]

            },
            {

                path: 'profile',
                component:ProfileComponent,
                canActivate :[authGuard]
            },
            {
                path:'changepassword',
                component:ChangePassowrdComponent,
                canActivate:[authGuard]
            },
            {
                path:'viewcart',
                component:ViewCartComponent,
                canActivate:[authGuard]

            },
            {
                path:'payment',
                component:PaymentComponent,
                canActivate:[authGuard]

            },
            {
                path:'invoice',
                component:InvoiceComponent,
                canActivate:[authGuard]

            },
            {
                
                    path:'changepassword',
                    component:ChangePassowrdComponent,
                    canActivate:[authGuard]
                
            },
            {
                path:'viewimg',
                component:ViewimageComponent,
                canActivate:[authGuard]
            }
        ]
    },
    {
        path: 'register',
        component:RegisterComponent
    },
    {
        path:'forgotpassword',
        component:ForgotPasswordComponent
    },
    
    
];
