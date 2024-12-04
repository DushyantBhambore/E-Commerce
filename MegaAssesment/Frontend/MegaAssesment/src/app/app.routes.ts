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
                component:DashboardComponent
            },
            {
                path:'product',
                component:ProductComponent
            },
            {

                path: 'profile',
                component:ProfileComponent
            },
            {
                path:'changepassword',
                component:ChangePassowrdComponent
            },
            {
                path:'viewcart',
                component:ViewCartComponent
            }
        ]
    },
    {
        path: 'register',
        component:RegisterComponent
    }
];
