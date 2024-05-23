import { Component, OnInit, inject } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss'],
})
export class NavComponent {
  #router = inject(Router);

  public isCollapsed = true;

  public showMenu(): boolean {
    return (this.#router.url !== '/usuarios/login' && this.#router.url !== '/usuarios/cadastro')
  }
}
