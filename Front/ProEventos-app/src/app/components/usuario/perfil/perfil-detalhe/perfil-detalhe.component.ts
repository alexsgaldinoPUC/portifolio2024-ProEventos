import { Component, EventEmitter, inject, OnInit, Output } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService, PalestranteService } from '../../../../services';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { UsuarioUpdate } from '../../../../interfaces/models';
import { FormValidator } from '../../../../util/class';

@Component({
  selector: 'app-perfil-detalhe',
  templateUrl: './perfil-detalhe.component.html',
  styleUrls: ['./perfil-detalhe.component.scss']
})
export class PerfilDetalheComponent {
  @Output() changeFormValue = new EventEmitter();

  #accountService = inject(AccountService)
  #palestranteService = inject(PalestranteService)
  #formBuilder = inject(FormBuilder);
  #spinnerService = inject(NgxSpinnerService);
  #toastrService = inject(ToastrService)

  public formPerfil = {} as FormGroup;

  public usuarioUpdate = {} as UsuarioUpdate;

  public get fc(): any {
    return this.formPerfil.controls;
  }

  public ngOnInit(): void {
    this.validation();
    this.carregarUsuario();
    this.verificarForm();
  }

  public validation(): void {
    const formOptions: AbstractControlOptions = {
      validators: FormValidator.argsCompare('password', 'confirmarPassword')
    }
    this.formPerfil = this.#formBuilder.group({
      titulo: ['', Validators.required],
      primeiroNome: ['', Validators.required],
      ultimoNome: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.required]],
      funcao: ['', Validators.required],
      descricao: ['', [Validators.required, Validators.minLength(20), Validators.maxLength(200)]],
      password: ['', [Validators.minLength(6), Validators.nullValidator]],
      confirmarPassword: ['', Validators.nullValidator],
      userName: [''],
      id: [''],
      imagemURL: ['']
    }, formOptions);
  }

  public resetForm(event: any): void {
    event.preventDefault();
    this.formPerfil.reset();
  }

  public fieldValidator(campoForm: FormControl): any {
    return FormValidator.checkFieldsWhithError(campoForm);
  }

  public messageReturned(nomeCampo: FormControl, nomeElemento: string): any {
    return FormValidator.returnMessage(nomeCampo, nomeElemento);
  }

  public carregarUsuario(): void {
    this.#spinnerService.show();

    this.#accountService
    .getUsuario()
    .subscribe({
      next: (usuarioUpdate: UsuarioUpdate) => {
        this.usuarioUpdate = usuarioUpdate;
        this.formPerfil.patchValue(this.usuarioUpdate);
        this.#toastrService.success("Conta carregada com sucesso!", "Sucesso!");
      },
      error: (error:any) => {
        console.error(error)
        this.#toastrService.error("Falha ao carregar a conta.", "Error")
      }
    })
    .add(() => this.#spinnerService.hide())
  }

  public onSubmit(): void {
    this.#spinnerService.show();

    this.usuarioUpdate = { ... this.formPerfil.value }

    if (this.fc.funcao.value == "Palestrante") {
      this.#palestranteService
        .createPalestrante()
        .subscribe({
          next: () => this.#toastrService.success("Função palestrante ativada!", "Sucesso!"),
          error: (error: any) => {
            console.log(error);
            this.#toastrService.error("Falha ao atualizar função do palestrante.", "Erro!")
          }
        })
    }

    this.#accountService
    .updateUsuario(this.usuarioUpdate)
    .subscribe({
      next: () => this.#toastrService.success("Conta atualizada!", "Sucesso!"),
      error: (error:any) => {
        console.error(error)
        this.#toastrService.error("Falha ao atualizar a conta.", "Error")
      }
    })
    .add(() => this.#spinnerService.hide())
  }

  private verificarForm(): void {
    this.formPerfil.valueChanges
      .subscribe({
        next: () => {this.changeFormValue.emit({ ... this.formPerfil.value })}
      })
  }
}
