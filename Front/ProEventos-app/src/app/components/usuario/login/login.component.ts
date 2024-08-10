import { Component, inject } from '@angular/core';
import { UsuarioLogin } from '../../../interfaces/models';
import { AccountService } from '../../../services';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  #accountService = inject(AccountService);
  #router = inject(Router);
  #spinnerService = inject(NgxSpinnerService)
  #toastrService = inject(ToastrService);

  public usuario = {} as UsuarioLogin;

  public login(): void {
    this.#spinnerService.show();

    this.#accountService.login(this.usuario)
      .subscribe({
        next: (userLogin) => {
          this.#router.navigateByUrl('/dashboard');
        },
        error: (error: any) => {
          console.log(error);
          (error.status == 401)
            ? this.#toastrService.error("Conta ou senha inválidos.", "Erro!")
            : this.#toastrService.error("Falha ao realizar login", "Erro!")
        }
      })
      .add(() => this.#spinnerService.hide());
  }
}
