import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../assets/environments/environment';
import { map, Observable, take } from 'rxjs';
import { PaginatedResult } from '../../interfaces/models/pagination/PaginatedResult';
import { Palestrante } from '../../interfaces/models';

@Injectable({
  providedIn: 'root'
})
export class PalestranteService {
  #http = inject(HttpClient);

  public baseUrl = `${environment.apiURL}palestrantes`;

  public getPalestrantes(page?: number, itemsPerPage?: number, argumento?: string): Observable<PaginatedResult<Palestrante[]>> {
    const paginatedResult: PaginatedResult<Palestrante[]> = new PaginatedResult<Palestrante[]>();

    let params = new HttpParams;

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    if (argumento != null && argumento != '') params = params.append('term', argumento);

    return this.#http
      .get<Palestrante[]>(this.baseUrl + '/todosPalestrantes', {observe: 'response', params})
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

  public getPalestrante(): Observable<Palestrante> {
    return this.#http
      .get<Palestrante>(`${this.baseUrl}`)
      .pipe(take(1));
  }

  public createPalestrante(): Observable<Palestrante> {
    return this.#http
      .post<Palestrante>(this.baseUrl, {} as Palestrante )
      .pipe(take(1));
  }

  public updatePalestrante(_palestrante: Palestrante): Observable<Palestrante> {
    return this.#http
      .put<Palestrante>(`${this.baseUrl}`, _palestrante)
      .pipe(take(1));
  }
}
