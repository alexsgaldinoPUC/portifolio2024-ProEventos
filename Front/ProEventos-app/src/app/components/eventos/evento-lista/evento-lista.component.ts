import { Component, OnInit, TemplateRef, inject } from '@angular/core';

import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

import { Evento } from '../../../interfaces/models';
import { EventoService } from '../../../services/evento';
import { Router } from '@angular/router';
import { environment } from '../../../../assets/environments/environment';
import { Pagination } from '../../../interfaces/models/pagination/Pagination';
import { PaginatedResult } from '../../../interfaces/models/pagination/PaginatedResult';
import { debounceTime, Subject } from 'rxjs';

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

  public pagination = {} as Pagination;

  private modalRef = {} as BsModalRef;

  public eventos = [] as Evento[];
  public eventoId = 0;

  public mostrarImagem = false;

  private termoBuscaChanged: Subject<string> = new Subject<string>();

  public alternarImagem(): void {
    this.mostrarImagem = !this.mostrarImagem;
  }

  public exibirImagem(imagemUrl: string): string {
    return (imagemUrl !== "" ? `${environment.imagemEventosURL}${imagemUrl}` : environment.imageDefault)
  }

  public filtrarEventos(event: any): void {
    if (this.termoBuscaChanged.observers.length == 0) {
      this.termoBuscaChanged.pipe(debounceTime(1000))
      .subscribe({
        next: (filtrarPor) => {
            this.#spinnerService.show();

            this.#eventoService.getEventos(this.pagination.currentPage, this.pagination.itemsPerPage, filtrarPor)
              .subscribe({
                next: (paginatedResponse: PaginatedResult<Evento[]>) => {
                  console.log(paginatedResponse)
                  this.pagination= paginatedResponse.pagination!;
                  this.eventos = paginatedResponse.result!;
                },
                error: error => {
                  console.log(error);
                  this.#toastrService.error("Falha ao recuperar eventos.", "Erro!")
                }
              })
              .add(() => this.#spinnerService.hide());
          }
        })
      }

      this.termoBuscaChanged.next(event.value)
  }

  public ngOnInit(): void {
    this.pagination = { currentPage: 1, itemsPerPage: 2, totalItems: 1 } as Pagination
    this.carregarEventos()
  }

  public carregarEventos(): void {
    this.#spinnerService.show();

    this.#eventoService
      .getEventos(this.pagination.currentPage, this.pagination.itemsPerPage)
      .subscribe({
        next: (paginatedResponse: PaginatedResult<Evento[]>) => {
          console.log(paginatedResponse)
          this.pagination= paginatedResponse.pagination!;
          this.eventos = paginatedResponse.result!;
        },
        error: error => {
          console.log(error);
          this.#toastrService.error("Falha ao recuperar eventos.", "Erro!")
        }
      })
      .add(() => this.#spinnerService.hide());
  }

  public openModal(_event: any, _template: TemplateRef<void>, _eventoId: number) {
    _event.stopPropagation();
    this.eventoId = _eventoId;
    this.modalRef = this.#modalService.show(_template, { class: 'modal-sm' });
  }

  public confirm(): void {
    this.modalRef.hide();
    this.#spinnerService.show();

    this.#eventoService
      .deleteEvento(this.eventoId)
      .subscribe({
        next: (result) => {
          if (result.message == "Excluido") {
            this.#toastrService.success("Exclusão de evento executada!", "Excluído!")
            this.carregarEventos();
          }
        },
        error: (error: any) => {
          console.error(error)
          this.#toastrService.error(`Erro ao excluir o evento ${this.eventoId}`, "Erro!")
        }
      })
      .add(() => this.#spinnerService.hide())

  }

  public decline(): void {
    this.modalRef.hide();
  }

  public detalheEvento(id: number): void {
    this.#router.navigate([`eventos/detalhe/${id}`])
  }

  public pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    console.log("event ", event.page);
    this.carregarEventos();
  }
}
