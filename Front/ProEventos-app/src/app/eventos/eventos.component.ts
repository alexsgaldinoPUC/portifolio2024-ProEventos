import { HttpClient } from '@angular/common/http';
import { Component, OnInit, inject } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrl: './eventos.component.scss',
})
export class EventosComponent {
  #htpp = inject(HttpClient)

  public eventos: any;

  ngOnInit(): void {
    this.getEventos()
  }

  public getEventos(): void {
    this.#htpp.get("http://localhost:5180/api/eventos")
      .subscribe({
        next: response => this.eventos = response,
        error: error => console.log(error)
      });
  }
}
