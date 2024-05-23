import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-usuario',
  templateUrl: './usuario.component.html',
  styleUrl: './usuario.component.scss'
})
export class UsuarioComponent {
  #router = inject(Router);

  public showTitulo(): boolean {
    return this.#router.url !== '/usuarios/login' && this.#router.url !== '/usuarios/cadastro'
  }

}
