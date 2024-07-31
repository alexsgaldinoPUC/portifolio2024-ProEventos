import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../assets/environments/environment';
import { Observable, take } from 'rxjs';
import { Lote } from '../../interfaces/models';

@Injectable()
export class LoteService {

  #http = inject(HttpClient);

  public baseUrl = `${environment.apiURL}Lotes`;

  public getLotesPorEventoId(_eventoId: number): Observable<Lote[]> {
    return this.#http
      .get<Lote[]>(`${this.baseUrl}/${_eventoId}`)
      .pipe(take(1));
  }

  public saveLote(_eventoId: number, _lotes: Lote[]): Observable<Lote[]> {
    return this.#http
      .put<Lote[]>(`${this.baseUrl}/${_eventoId}`, _lotes)
      .pipe(take(1));
  }

  public deleteLote(_eventoId: number, _loteId: number): Observable<any> {
    return this.#http
      .delete(`${this.baseUrl}/${_eventoId}/${_loteId}`)
      .pipe(take(1));
  }

}
