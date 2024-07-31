import { ActivatedRoute, Router } from '@angular/router';
import { Component, inject, TemplateRef } from '@angular/core';
import {
  AbstractControl,
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';

import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { FormValidator } from '../../../util/class';

import { Evento, Lote } from '../../../interfaces/models';

import { EventoService, LoteService } from '../../../services';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrl: './evento-detalhe.component.scss',
})
export class EventoDetalheComponent {
  #activateRouterService = inject(ActivatedRoute);
  #eventoService = inject(EventoService);
  #loteService = inject(LoteService);
  #fromBuilder = inject(FormBuilder);
  #localeService = inject(BsLocaleService);
  #modalService = inject(BsModalService);
  #routerService = inject(Router);
  #spinnerService = inject(NgxSpinnerService);
  #toastrService = inject(ToastrService);

  public modalRef = {} as BsModalRef;

  public formEvento = {} as FormGroup;
  public formLotes = {} as FormGroup;

  private eventoIdParam: any = '';

  public evento = {} as Evento;
  public loteAtual = { id: 0, nome: '', indice: 0 };

  public modoEditar = false;

  public get bsConfig(): any {
    return {
      isAnimated: true,
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY hh:mm a',
      containerClass: 'theme-default',
    };
  }

  public get bsConfigLote(): any {
    return {
      isAnimated: true,
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY',
      containerClass: 'theme-default',
    };
  }

  public mudarValorData(valor: Date, indice: number, campo: string): void {
    this.lotes.value[indice][campo] = valor;
  }

  public get lotes(): FormArray {
    return this.formLotes.get('lotes') as FormArray;
  }

  public get fc(): any {
    return this.formEvento.controls;
  }

  public constructor() {
    this.#localeService.use('pt-br');
  }

  public ngOnInit(): void {
    this.eventoIdParam =
      this.#activateRouterService?.snapshot.paramMap.get('id');
    this.modoEditar = this.eventoIdParam != null ? true : false;

    this.validation();
    if (this.modoEditar) this.carregarEvento();
  }

  public validation(): void {
    this.formEvento = this.#fromBuilder.group({
      local: ['', Validators.required],
      dataEvento: ['', Validators.required],
      tema: [
        '',
        [
          Validators.required,
          Validators.minLength(4),
          Validators.maxLength(50),
        ],
      ],
      qtdePessoas: ['', [Validators.required, Validators.max(120000)]],
      imagemUrl: ['', Validators.required],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
    });
    this.formLotes = this.#fromBuilder.group({
      lotes: this.#fromBuilder.array([]),
    });
  }

  public adicionarLote(): void {
    this.lotes.push(this.criarLote({ id: 0 } as Lote));
  }

  public criarLote(lote: Lote): FormGroup {
    return this.#fromBuilder.group({
      id: [lote.id],
      nome: [lote.nome, Validators.required],
      quantidade: [lote.quantidade, Validators.required],
      preco: [lote.preco, Validators.required],
      dataInicioLote: [lote.dataInicioLote],
      dataFimLote: [lote.dataFimLote],
    });
  }

  public carregarEvento(): void {
    this.#spinnerService.show();

    this.#eventoService
      .getEventoPorId(+this.eventoIdParam)
      .subscribe({
        next: (evento: Evento) => {
          this.evento = { ...evento };
          this.formEvento.patchValue(this.evento);
          console.log(this.evento);

          evento.lotes.forEach((element) => {
            this.lotes.push(this.criarLote(element));
          });
        },
        error: (error: any) => {
          console.error(error);
          this.#toastrService.error('Erro ao carregar Evento', 'Erro!');
        },
      })
      .add(() => this.#spinnerService.hide());
  }

  public salvarEvento(): void {
    this.#spinnerService.show();

    if (this.formEvento.valid)
      this.modoEditar ? this.alterarEvento() : this.criarEvento();
  }

  public criarEvento(): void {
    this.evento = { ...this.formEvento.value };

    this.#eventoService
      .createEvento(this.evento)
      .subscribe({
        next: (evento: Evento) => {
          this.#routerService.navigate([`eventos/detalhe/${evento.id}`]);
          this.#toastrService.success('Evento criado com sucesso!', 'Criado!');
        },
        error: (error: any) => {
          console.error(error);
          this.#toastrService.error('Falha ao cadastrar evento.', 'Erro!');
        },
      })
      .add(() => this.#spinnerService.hide());
  }

  public salvarLotes(): void {
    if (this.formLotes.valid) {
      this.#spinnerService.show();
      console.log(this.eventoIdParam);
      console.log(this.formLotes.value.lotes);
      this.#loteService
        .saveLote(this.eventoIdParam, this.formLotes.value.lotes)
        .subscribe({
          next: () => {
            this.#toastrService.success(
              'Lotes salvos com sucesso!',
              'Sucesso!'
            );
            this.lotes.reset();
          },
          error: (error: any) => {
            console.error(error);
            this.#toastrService.error('Falha ao salvar lotes.', 'Erro!');
          },
        })
        .add(() => this.#spinnerService.hide());
    }
  }

  public alterarEvento(): void {
    this.evento = { id: +this.eventoIdParam, ...this.formEvento.value };

    this.#eventoService
      .updateEvento(this.evento)
      .subscribe({
        next: (evento: Evento) => {
          this.#toastrService.success('Evento criado com sucesso!', 'Criado!');
        },
        error: (error: any) => {
          console.error(error);
          this.#toastrService.error('Falha ao cadastrar evento.', 'Erro!');
        },
      })
      .add(() => this.#spinnerService.hide());
  }

  public removerLote(template: TemplateRef<any>, indice: number): void {
    this.loteAtual.id = this.lotes.get(indice + '.id')?.value;
    this.loteAtual.nome = this.lotes.get(indice + '.nome')?.value;
    this.loteAtual.indice = indice;

    this.modalRef = this.#modalService.show(template, { class: 'modal-sm' });
  }

  public confirmDeleteLote(): void {
    this.modalRef.hide();
    this.#spinnerService.show();

    this.#loteService
      .deleteLote(this.eventoIdParam, this.loteAtual.id)
      .subscribe({
        next: (retorno: any) => {
          this.lotes.removeAt(this.loteAtual.indice);
          this.#toastrService.success('Lote exclído com sucesso!', 'Sucesso!');
        },
        error: (error: any) => {
          this.#toastrService.error(
            `Falha ao excluir lote ${this.loteAtual.nome}.`,
            'Erro!'
          );
        },
      })
      .add(() => this.#spinnerService.hide());
  }

  public declineDeleteLote(): void {
    this.modalRef.hide();
  }

  public retornaTituloLote(indice: number): string {
    return this.lotes.get(indice+'.nome')?.value === null || this.lotes.get(indice+'.nome')?.value == '' ? 'Nome do Lote' : this.lotes.get(indice+'.nome')?.value
  }
  public resetForm(): void {
    this.formEvento.reset();
  }

  public fieldValidator(campoForm: FormControl | AbstractControl | null): any {
    return FormValidator.checkFieldsWhithError(campoForm);
  }

  public messageReturned(
    nomeCampo: FormControl | AbstractControl | null,
    nomeElemento: string
  ): any {
    return FormValidator.returnMessage(nomeCampo, nomeElemento);
  }
}
