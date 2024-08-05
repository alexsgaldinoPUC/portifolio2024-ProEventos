import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../../assets/environments/environment';
import { Observable, take } from 'rxjs';
import { Evento } from '../../interfaces/models/eventos/Evento';

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

}
