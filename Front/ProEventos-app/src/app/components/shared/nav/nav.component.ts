import { Component, OnInit, inject } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../../../services';
import { Usuario } from '../../../interfaces/models';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss'],
})
export class NavComponent implements OnInit{
  #accountService = inject(AccountService)
  #router = inject(Router);

  public usuarioLogado = false;
  public usuario = {} as Usuario

  ngOnInit(): void {
    this.verificarUsuarioLogado();
  }

  public isCollapsed = true;

  public showMenu(): boolean {
    //return (this.#router.url !== '/usuarios/login' && this.#router.url !== '/usuarios/cadastro')
    return (this.#router.url !== '/usuarios/login' )
  }

  public logout(): void {
    this.#accountService.logout();
    this.#router.navigateByUrl('/usuarios/login');
  }

  public verificarUsuarioLogado(): void {
    console.log("aqui", this.#accountService.currentUSer$)
    this.#accountService.currentUSer$.subscribe({
      next: (usuario: Usuario) => {
        console.log("aqui2")
        this.usuario = usuario;
        this.usuario ? this.usuarioLogado = true : this.usuarioLogado = false;
        console.log(this.usuarioLogado)
        console.log(this.usuario)
      }
    })
  }
}
