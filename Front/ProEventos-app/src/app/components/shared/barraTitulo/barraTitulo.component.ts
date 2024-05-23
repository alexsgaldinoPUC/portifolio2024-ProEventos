import { Component, Input, OnInit, inject } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-barraTitulo',
  templateUrl: './barraTitulo.component.html',
  styleUrls: ['./barraTitulo.component.scss'],
})
export class BarraTituloComponent {
  #router = inject(Router);

  @Input() titulo = '';
  @Input() iconeTitulo = 'fa fa-user';
  @Input() subTitulo = 'desde 2021 '
  @Input() botaoListar = false;

  public listar(): void {
    this.#router.navigate([`/${this.titulo.toLocaleLowerCase()}/lista`])
  }
}
