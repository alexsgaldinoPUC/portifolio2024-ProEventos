import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../../assets/environments/environment';
import { Observable, take } from 'rxjs';
import { Evento } from '../../interfaces/models/eventos/Evento';
import { UsuarioUpdate } from '../../interfaces/models';

@Injectable()
export class UploadService {
  #http = inject(HttpClient);

  public baseUrl = `${environment.apiURL}Uploads`;

  public uploadImagem(_eventoId: number, file: any): Observable<Evento> {
    const fileToUpload = file[0] as File;
    const formData = new FormData();

    formData.append('file', fileToUpload);

    return this.#http
      .post<Evento>(`${this.baseUrl}/upload-image/${_eventoId}`, formData)
      .pipe(take(1));
  }

  public uploadPerfil(file: any): Observable<UsuarioUpdate> {
    const fileToUpload = file[0] as File;
    const formData = new FormData();

    formData.append('file', fileToUpload);

    return this.#http
      .post<UsuarioUpdate>(`${this.baseUrl}/upload-perfil`, formData)
      .pipe(take(1));
  }

}
