import { Component, inject, Input, OnInit, TemplateRef } from '@angular/core';
import {
  AbstractControl,
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { RedeSocial } from '../../interfaces/models';
import { FormValidator } from '../../util/class';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { RedeSocialService } from '../../services/redeSocial/rede-social.service';

@Component({
  selector: 'app-redes-sociais',
  templateUrl: './redes-sociais.component.html',
  styleUrls: ['./redes-sociais.component.scss'],
})
export class RedesSociaisComponent {
  #fromBuilder = inject(FormBuilder);
  #modalService = inject(BsModalService);
  #redeSocialService = inject(RedeSocialService);
  #spinnerService = inject(NgxSpinnerService);
  #toastrService = inject(ToastrService);

  @Input() eventoId = 0;

  public formRedesSociais = {} as FormGroup;

  public modalRef = {} as BsModalRef;


  public redeSocialAtual = { id: 0, nome: '', indice: 0 };

  public get redesSociais(): FormArray {
    return this.formRedesSociais.get('redesSociais') as FormArray;
  }

  public ngOnInit(): void {
    this.validation();

    this.carregarRedesSociais(this.eventoId);
  }

  public validation(): void {
    this.formRedesSociais = this.#fromBuilder.group({
      redesSociais: this.#fromBuilder.array([]),
    });
  }

  public carregarRedesSociais(id: number = 0): void {
    this.#spinnerService.show();

    let origem = 'palestrante';

    if (this.eventoId != 0) origem = 'evento';

    this.#redeSocialService
      .getRedesSociais(origem, id)
      .subscribe({
        next: (redesSociais: RedeSocial[]) => {
          redesSociais.forEach((element) => {
            this.redesSociais.push(this.criarRedeSocial(element));
          });
        },
        error: (error: any) => {
          console.error(error);
          this.#toastrService.error('Erro ao carregar Redes Sociais', 'Erro!');
        },
      })
      .add(() => this.#spinnerService.hide());
  }

  public adicionarRedeSocial(): void {
    this.redesSociais.push(this.criarRedeSocial({ id: 0 } as RedeSocial));
  }

  public criarRedeSocial(redeSocial: RedeSocial): FormGroup {
    return this.#fromBuilder.group({
      id: [redeSocial.id],
      nome: [redeSocial.nome, Validators.required],
      url: [redeSocial.url, Validators.required],
    });
  }

  public retornaTitulo(indice: number): string {
    return this.redesSociais.get(indice + '.nome')?.value === null ||
      this.redesSociais.get(indice + '.nome')?.value == ''
      ? 'Nome da Rede Social'
      : this.redesSociais.get(indice + '.nome')?.value;
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

  public salvarRedeSocial(): void {
    if (this.formRedesSociais.valid) {
      this.#spinnerService.show();

      let origem = 'palestrante';

      if (this.eventoId != 0) origem = 'evento';

      this.#redeSocialService
        .saveRedesSociais(origem, this.eventoId, this.formRedesSociais.value.redesSociais)
        .subscribe({
          next: () => {
            this.#toastrService.success(
              'Redes Sociais salvas com sucesso!',
              'Sucesso!'
            );
            //this.redesSociais.reset();
          },
          error: (error: any) => {
            console.error(error);
            this.#toastrService.error('Falha ao salvar Redes Sociais.', 'Erro!');
          },
        })
        .add(() => this.#spinnerService.hide());
    }
  }

  public removerRedeSocial(template: TemplateRef<any>, indice: number): void {
    this.redeSocialAtual.id = this.redesSociais.get(indice + '.id')?.value;
    this.redeSocialAtual.nome = this.redesSociais.get(indice + '.nome')?.value;
    this.redeSocialAtual.indice = indice;

    this.modalRef = this.#modalService.show(template, { class: 'modal-sm' });
  }

  public confirmDeleteLote(): void {
    this.#spinnerService.show();
    this.modalRef.hide();

    let origem = 'palestrante';

    if (this.eventoId != 0) origem = 'evento';

    this.#redeSocialService
      .deleteRedeSocial(origem, this.eventoId, this.redeSocialAtual.id)
      .subscribe({
        next: (retorno: any) => {
          this.redesSociais.removeAt(this.redeSocialAtual.indice);
          this.#toastrService.success('Rede Social excluído com sucesso!', 'Sucesso!');
        },
        error: (error: any) => {
          this.#toastrService.error(
            `Falha ao excluir Rede Social ${this.redeSocialAtual.nome}.`,
            'Erro!'
          );
        },
      })
      .add(() => this.#spinnerService.hide());
  }

  public declineDeleteLote(): void {
    this.modalRef.hide();
  }

  public resetForm(): void {
    this.formRedesSociais.reset();
  }
}
