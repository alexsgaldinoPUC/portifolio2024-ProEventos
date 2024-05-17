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
  public eventosFiltrados: any = [];

  public mostrarImagem = true;

  private _filtroLista = "";

  public alternarImagem(): void {
    this.mostrarImagem = !this.mostrarImagem;
  }

  public get filtroLista(): string {
    return this._filtroLista;
  }

  public set filtroLista(argumento: string)  {
    this._filtroLista = argumento
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }

  public filtrarEventos(filtrarPor: string): any {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      (evento: any) => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1  ||
                       evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }

  ngOnInit(): void {
    this.getEventos()
  }

  public getEventos(): void {
    this.#htpp.get("http://localhost:5180/api/eventos")
      .subscribe({
        next: response => {
          this.eventos = response;
          this.eventosFiltrados = this.eventos;
        },
        error: error => console.log(error)
      });
  }
}
