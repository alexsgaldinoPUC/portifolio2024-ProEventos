import { ActivatedRoute } from '@angular/router';
import { Component, inject } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';

import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { FormValidator } from '../../../util/class';

import { Evento } from '../../../interfaces/models';

import { EventoService } from '../../../services/evento';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrl: './evento-detalhe.component.scss',
})
export class EventoDetalheComponent {
  #fromBuilder = inject(FormBuilder);
  #localeService = inject(BsLocaleService);
  #routerService = inject(ActivatedRoute);
  #eventoService = inject(EventoService);
  #spinnerService = inject(NgxSpinnerService);
  #toastrService = inject(ToastrService);

  public formEvento = {} as FormGroup;

  private eventoIdParam: any = '';

  public evento = {} as Evento;

  public modoEditar = false;

  public get bsConfig(): any {
    return {
      isAnimated: true,
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY hh:mm a',
      containerClass: 'theme-default',
    };
  }

  public get fc(): any {
    return this.formEvento.controls;
  }

  public constructor() {
    this.#localeService.use('pt-br');
  }

  public ngOnInit(): void {
    this.eventoIdParam = this.#routerService?.snapshot.paramMap.get('id');
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
  }

  public carregarEvento(): void {
    this.#spinnerService.show();

    this.#eventoService
      .getEventoPorId(+this.eventoIdParam)
      .subscribe({
        next: (evento: Evento) => {
          this.evento = { ...evento };
          this.evento.lotes = [];
          this.evento.palestrantesEventos = [];
          this.evento.redesSociais = [];
          this.formEvento.patchValue(this.evento);
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
          this.#toastrService.success('Evento criado com sucesso!', 'Criado!');
        },
        error: (error: any) => {
          console.error(error);
          this.#toastrService.error('Falha ao cadastrar evento.', 'Erro!');
        },
      })
      .add(() => this.#spinnerService.hide());
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

  public resetForm(): void {
    this.formEvento.reset();
  }

  public fieldValidator(campoForm: FormControl): any {
    return FormValidator.checkFieldsWhithError(campoForm);
  }

  public messageReturned(nomeCampo: FormControl, nomeElemento: string): any {
    return FormValidator.returnMessage(nomeCampo, nomeElemento);
  }
}
