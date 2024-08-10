import { Component, inject } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { FormValidator } from '../../../util/class';
import { Usuario } from '../../../interfaces/models';
import { NgxSpinnerService } from 'ngx-spinner';
import { AccountService } from '../../../services';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-cadastro',
  templateUrl: './cadastro.component.html',
  styleUrl: './cadastro.component.scss',
})
export class CadastroComponent {
  #accountService = inject(AccountService);
  #formBuilder = inject(FormBuilder);
  #router = inject(Router);
  #spinnerService = inject(NgxSpinnerService);
  #toastrService = inject(ToastrService);

  public formCadastro = {} as FormGroup;

  public usuario = {} as Usuario;

  public get fc(): any {
    return this.formCadastro.controls;
  }

  public ngOnInit(): void {
    this.validation();
  }

  public validation(): void {
    const formOptions: AbstractControlOptions = {
      validators: FormValidator.argsCompare('password', 'confirmarPassword')
    }
    this.formCadastro = this.#formBuilder.group({
      nome: ['', Validators.required],
      sobrenome: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      userName: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmarPassword: ['', Validators.required],
    }, formOptions);
  }

  public fieldValidator(campoForm: FormControl): any {
    return FormValidator.checkFieldsWhithError(campoForm);
  }

  public messageReturned(nomeCampo: FormControl, nomeElemento: string): any {
    return FormValidator.returnMessage(nomeCampo, nomeElemento);
  }

  public cadastrar(): void {
    this.#spinnerService.show();

    this.usuario = { ...this.formCadastro.value };

    this.#accountService.cadastrar(this.usuario)
      .subscribe({
        next: () => this.#router.navigateByUrl('/dashboard'),
        error: (error:any) => {
          console.log(error);
          this.#toastrService.error("Falha ao cadastrar conta", "Erro!")
        }
      })
      .add(() => this.#spinnerService.hide())
  }
}
