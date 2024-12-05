import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {

  const token = localStorage.getItem('token'); // Check if a token exists
  const role = localStorage.getItem('role'); // Check user role from local storage

  const router = inject(Router);
  if (token) {

    if(role ==='admin')
    {
      router.navigateByUrl('/product');
    }
    return true;
  }

  if(token)
  {
    router.navigateByUrl('/dashboard');
    return true;
  }

  router.navigateByUrl('/login');
  return false;
};
