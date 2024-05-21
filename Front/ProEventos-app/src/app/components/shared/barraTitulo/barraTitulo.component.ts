import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-barraTitulo',
  templateUrl: './barraTitulo.component.html',
  styleUrls: ['./barraTitulo.component.scss'],
})
export class BarraTituloComponent {
  @Input() titulo = '';
}
