import { Component, inject, OnInit } from '@angular/core';
import { PalestranteService } from '../../../services';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { Router } from '@angular/router';
import { Pagination } from '../../../interfaces/models/pagination/Pagination';
import { Palestrante } from '../../../interfaces/models';
import { debounceTime, Subject } from 'rxjs';
import { PaginatedResult } from '../../../interfaces/models/pagination/PaginatedResult';
import { environment } from '../../../../assets/environments/environment';

@Component({
  selector: 'app-palestrante-lista',
  templateUrl: './palestrante-lista.component.html',
  styleUrls: ['./palestrante-lista.component.scss']
})
export class PalestranteListaComponent implements OnInit {
  #palestranteService = inject(PalestranteService);
  #modalService = inject(BsModalService);
  #router = inject(Router);
  #spinnerService = inject(NgxSpinnerService);
  #toastrService = inject(ToastrService);

  public pagination = {} as Pagination;

  private modalRef = {} as BsModalRef;

  public palestrantes = [] as Palestrante[];

  public imagemURL = environment.imageDefault

  private termoBuscaChanged: Subject<string> = new Subject<string>();

  public getImagemURL(imageName: string): string {
    return (imageName != null) ? environment.imagemPerfilURL + imageName : this.imagemURL
  }

  public filtrarPalestrantes(event: any): void {
    if (this.termoBuscaChanged.observers.length == 0) {
      this.termoBuscaChanged.pipe(debounceTime(1000))
      .subscribe({
        next: (filtrarPor) => {
            this.#spinnerService.show();

            this.#palestranteService.getPalestrantes(this.pagination.currentPage, this.pagination.itemsPerPage, filtrarPor)
              .subscribe({
                next: (paginatedResponse: PaginatedResult<Palestrante[]>) => {
                  console.log(paginatedResponse)
                  this.pagination= paginatedResponse.pagination!;
                  this.palestrantes = paginatedResponse.result!;
                },
                error: error => {
                  console.log(error);
                  this.#toastrService.error("Falha ao recuperar palestrantes.", "Erro!")
                }
              })
              .add(() => this.#spinnerService.hide());
          }
        })
      }

      this.termoBuscaChanged.next(event.value)
  }

  public ngOnInit(): void {
    this.carregarPalestrantes()
  }

  public carregarPalestrantes(): void {
    this.#spinnerService.show();

    this.#palestranteService
      .getPalestrantes(this.pagination.currentPage, this.pagination.itemsPerPage)
      .subscribe({
        next: (paginatedResponse: PaginatedResult<Palestrante[]>) => {
          console.log(paginatedResponse)
          this.pagination= paginatedResponse.pagination!;
          this.palestrantes = paginatedResponse.result!;
        },
        error: error => {
          console.log(error);
          this.#toastrService.error("Falha ao recuperar palestrantes.", "Erro!")
        }
      })
      .add(() => this.#spinnerService.hide());
  }
}
