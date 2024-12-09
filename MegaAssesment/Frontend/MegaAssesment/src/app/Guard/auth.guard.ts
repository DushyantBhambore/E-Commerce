import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {

  const token = localStorage.getItem('token'); // Check if a token exists
  const role =localStorage.getItem('role'); // Check user role from local storage

  const router = inject(Router);
  if (token) {

    if(role === '4')
    {
      router.navigateByUrl('/profile');
    }
    return true;
  }

  if(token)
  {
    router.navigateByUrl('/profile');
    return true;
  }

  router.navigateByUrl('/login');
  return false;
};
