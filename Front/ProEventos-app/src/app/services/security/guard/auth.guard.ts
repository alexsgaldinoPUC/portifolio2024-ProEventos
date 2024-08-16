import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { environment } from '../../../../assets/environments/environment';

export const AuthGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const toastrService = inject(ToastrService);

  if (localStorage.getItem(environment.localStorageName) != null)
      return true;

  toastrService.info("Conta não autenticada!", "Info!");
  router.navigate(["/usuarios/login"]);

  return false;
};

