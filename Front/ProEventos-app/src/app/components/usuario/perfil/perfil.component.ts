import { Component, OnInit, inject } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { FormValidator } from '../../../util/class';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent implements OnInit {
  #fromBuilder = inject(FormBuilder);

  public formPerfil = {} as FormGroup;

  public get fc(): any {
    return this.formPerfil.controls;
  }

  public ngOnInit(): void {
    this.validation();
    console.log(this.fc.titulo.value)
  }
  public validation(): void {
    const formOptions: AbstractControlOptions = {
      validators: FormValidator.argsCompare('password', 'confirmarPassword')
    }
    this.formPerfil = this.#fromBuilder.group({
      titulo: ['', Validators.required],
      nome: ['', Validators.required],
      sobrenome: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      telefone: ['', [Validators.required]],
      funcao: ['', Validators.required],
      descricao: ['', [Validators.required, Validators.minLength(20), Validators.maxLength(200)]],
      password: ['', [Validators.required, Validators.minLength(6), ]],
      confirmarPassword: ['', Validators.required],
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


}
