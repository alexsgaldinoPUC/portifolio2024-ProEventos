import { HttpInterceptorFn } from '@angular/common/http';
import { AccountService } from '../../Usuario';
import { inject } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Usuario } from '../../../interfaces/models';
import { take } from 'rxjs';

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  const accountService = inject(AccountService);
  const toastrService = inject(ToastrService);

  let currentUser = {} as Usuario;

  accountService.currentUSer$.pipe(take(1)).subscribe({
    next: (usuario: any) => {
      currentUser = usuario;

      if (currentUser && currentUser.token) {
        req = req.clone({
          setHeaders: {
            Authorization: `Bearer ${currentUser.token}`,
          },
        });
      }
    },
    error: (error: any) => {
      console.log(error);
      toastrService.error('Falha interceptor', 'Error!');
    },
  });

  return next(req);
};
