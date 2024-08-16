import { Component, OnInit, inject } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { FormValidator } from '../../../util/class';
import { AccountService, UploadService } from '../../../services';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { UsuarioUpdate } from '../../../interfaces/models';
import { environment } from '../../../../assets/environments/environment';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent {
  #toastrService = inject(ToastrService);
  #spinnerService = inject(NgxSpinnerService);
  #uploadService = inject(UploadService);

  public usuario = {} as UsuarioUpdate;

  public imagemURL = environment.imageDefault

  public get ehPalestrante(): boolean {
    return this.usuario.funcao == 'Palestrante'
  }

  public getFormValue(usuario: UsuarioUpdate): void {
    this.usuario = usuario;

    this.imagemURL = (this.usuario.imagemURL != '')
      ? environment.imagemPerfilURL + this.usuario.imagemURL
      : this.imagemURL

    console.log(this.imagemURL, ' ', )
  }

  public onFileChange(event: any): void {
    const reader = new FileReader();

    reader.onload = (img: any) => this.imagemURL = img.target.result;

    const file = event.target.files;

    reader.readAsDataURL(file[0]);

    this.uploadPerfil(file);
  }

  public uploadPerfil(file: File): void {
    this.#spinnerService.show();

    this.#uploadService
      .uploadPerfil(file)
      .subscribe({
        next: () => {
          this.#toastrService.success('Imagem do perfil atualizada com sucesso!', 'Sucesso!')
        },
        error: (error: any) => {
          this.#toastrService.error("Falha ao atualizar a imagem do perfil.", "Erro!");
          console.log(error)
        }
      })
      .add(() => this.#spinnerService.hide());
  }
}
