import { HttpInterceptorFn } from '@angular/common/http';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const tokendata = localStorage.getItem('token')
  if(tokendata !=null)
  {
    debugger
    const clonereq = req.clone({
      setHeaders:{
        Authorization: `Bearer ${tokendata}`
      }
    });
    return next(clonereq);  
  }
  return next(req);
};
