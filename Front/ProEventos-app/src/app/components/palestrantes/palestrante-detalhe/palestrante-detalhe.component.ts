import { Component, inject, OnInit } from '@angular/core';
import {
  AbstractControlOptions,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { PalestranteService } from '../../../services';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { FormValidator } from '../../../util/class';
import { debounce, debounceTime, map, tap } from 'rxjs';
import { Palestrante } from '../../../interfaces/models';

@Component({
  selector: 'app-palestrante-detalhe',
  templateUrl: './palestrante-detalhe.component.html',
  styleUrls: ['./palestrante-detalhe.component.scss'],
})
export class PalestranteDetalheComponent {
  #formBuilder = inject(FormBuilder);
  #palestranteService = inject(PalestranteService);
  #spinnerService = inject(NgxSpinnerService);
  #toastrService = inject(ToastrService);

  public formPalestrante = {} as FormGroup;

  public situacaoForm = '';
  public corDescricao = '';

  public get fc(): any {
    return this.formPalestrante.controls;
  }

  public ngOnInit(): void {
    this.validation();
    this.verificarForm();
    this.carregarPalestrante();
  }

  private validation(): void {
    this.formPalestrante = this.#formBuilder.group({
      miniCurriculo: ['', Validators.required],
    });
  }

  public fieldValidator(campoForm: FormControl): any {
    return FormValidator.checkFieldsWhithError(campoForm);
  }

  public messageReturned(nomeCampo: FormControl, nomeElemento: string): any {
    return FormValidator.returnMessage(nomeCampo, nomeElemento);
  }

  private verificarForm(): void {
    this.formPalestrante.valueChanges
      .pipe(
        map(() => {
          this.situacaoForm = 'Mini currículo está em atualização!';
          this.corDescricao = 'text-warning';
        }),
        debounceTime(1000),
        tap(() => this.#spinnerService.show())
      )
      .subscribe({
        next: () => {
          this.#palestranteService
            .updatePalestrante({ ... this.formPalestrante.value })
            .subscribe({
              next: () => {
                this.situacaoForm = 'Mini currículo atualizado!';
                this.corDescricao = 'text-success';

                setTimeout(
                  () => {
                    this.situacaoForm = 'Mini currículo carregado!';
                    this.corDescricao = 'text-muted';
                  }, 2000
                )
              },
              error: (error) => {
                console.log(error)
                this.#toastrService.error("Falha ao atualizar palestrante!", "Erro!")
              }
            })
            .add(() => this.#spinnerService.hide())
        },
      });
  }

  private carregarPalestrante(): void {
    this.#spinnerService.show();

    this.#palestranteService
      .getPalestrante()
      .subscribe({
        next: (palestrante: Palestrante) => {
          this.formPalestrante.patchValue(palestrante);
          this.#toastrService.success("Palestrante carregado com sucesso!", "Sucesso!")
        },
        error: (error) => {
          this.#toastrService.error("Falha ao carregar Palestrante!", "Erro!")
          console.log(error)
        }
      })
  }
}
