import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../../assets/environments/environment';
import { map, Observable, take } from 'rxjs';
import { Evento } from '../../interfaces/models/eventos/Evento';
import { PaginatedResult } from '../../interfaces/models/pagination/PaginatedResult';

@Injectable()
export class EventoService {
  #http = inject(HttpClient);

  public baseUrl = `${environment.apiURL}Eventos`;

  public getEventos(page?: number, itemsPerPage?: number, argumento?: string): Observable<PaginatedResult<Evento[]>> {
    const paginatedResult: PaginatedResult<Evento[]> = new PaginatedResult<Evento[]>();

    let params = new HttpParams;

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    if (argumento != null && argumento != '') params = params.append('term', argumento);

    return this.#http
      .get<Evento[]>(this.baseUrl, {observe: 'response', params})
      .pipe(take(1), map(
        (response) => {
          paginatedResult.result = response.body || [];
          if (response.headers.has('Pagination')) {
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination')!);
          }
          return paginatedResult;
        }
      ));
  }

  public getEventoPorId(_id: number): Observable<Evento> {
    return this.#http
      .get<Evento>(`${this.baseUrl}/${_id}`)
      .pipe(take(1));
  }

  public createEvento(_evento: Evento): Observable<Evento> {
    return this.#http
      .post<Evento>(this.baseUrl, _evento)
      .pipe(take(1));
  }

  public updateEvento(_evento: Evento): Observable<Evento> {
    return this.#http
      .put<Evento>(`${this.baseUrl}/${_evento.id}`, _evento)
      .pipe(take(1));
  }

  public deleteEvento(_id: number): Observable<any> {
    return this.#http
      .delete(`${this.baseUrl}/${_id}`)
      .pipe(take(1));
  }
}
