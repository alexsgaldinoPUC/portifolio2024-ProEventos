import { Component, inject, OnInit } from '@angular/core';
import { AccountService } from './services';
import { NgxSpinnerService } from 'ngx-spinner';
import { Usuario } from './interfaces/models';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  #accountService = inject(AccountService);
  #spinnerService = inject(NgxSpinnerService)

  public title = 'ProEventos-app';

  public ngOnInit(): void {
    this.#spinnerService.show();

    let usuario = {} as Usuario;

    if (localStorage.getItem("proeventos-api")) {
      usuario = JSON.parse(localStorage.getItem("proeventos-api") ?? "{}")
    }

    console.log("AppComponent", usuario)
    if (usuario) this.#accountService.setCurrentUser(usuario);

    this.#spinnerService.hide();
  }
}
