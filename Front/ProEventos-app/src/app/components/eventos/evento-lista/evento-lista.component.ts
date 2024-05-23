import { Component, OnInit, TemplateRef, inject } from '@angular/core';

import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

import { Evento } from '../../../interfaces/models';
import { EventoService } from '../../../services/evento';
import { Router } from '@angular/router';

@Component({
  selector: 'app-evento-lista',
  templateUrl: './evento-lista.component.html',
  styleUrl: './evento-lista.component.scss'
})
export class EventoListaComponent {
  #eventoService = inject(EventoService);
  #modalService = inject(BsModalService);
  #router = inject(Router);
  #spinnerService = inject(NgxSpinnerService);
  #toastrService = inject(ToastrService);

  public modalRef = {} as BsModalRef;

  public eventos = [] as Evento[];
  public eventosFiltrados = [] as Evento[];

  public mostrarImagem = false;

  private filtroListado = "";

  public alternarImagem(): void {
    this.mostrarImagem = !this.mostrarImagem;
  }

  public get filtroLista(): string {
    return this.filtroListado;
  }

  public set filtroLista(argumento: string)  {
    this.filtroListado = argumento
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }

  public filtrarEventos(filtrarPor: string): Evento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      (evento: any) => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1  ||
                       evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  public ngOnInit(): void {
    this.getEventos()
  }

  public getEventos(): void {
    this.#spinnerService.show();

    this.#eventoService.getEventos()
      .subscribe({
        next: (eventos: Evento[]) => {
          this.eventos = eventos;
          this.eventosFiltrados = this.eventos;
        },
        error: error => {
          console.log(error);
          this.#toastrService.error("Falha ao recuperar eventos.", "Erro!")
        }
      })
      .add(() => this.#spinnerService.hide());
  }

  public openModal(template: TemplateRef<void>) {
    this.modalRef = this.#modalService.show(template, { class: 'modal-sm' });
  }

  public confirm(): void {
    this.modalRef.hide();
    this.#toastrService.info("Exclusão de evento executada!", "Informação")
  }

  public decline(): void {
    this.modalRef.hide();
  }

  public detalheEvento(id: number): void {
    this.#router.navigate([`eventos/detalhe/${id}`])
  }
}
