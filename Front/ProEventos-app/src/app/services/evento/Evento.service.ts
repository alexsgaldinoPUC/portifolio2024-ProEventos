import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../../assets/environments/environment';
import { Observable, take } from 'rxjs';
import { Evento } from '../../interfaces/models/eventos/Evento';

@Injectable()
export class EventoService {
  #http = inject(HttpClient);

  public baseUrl = `${environment.apiURL}Eventos`;

  public getEventos(): Observable<Evento[]> {
    return this.#http
      .get<Evento[]>(this.baseUrl)
      .pipe(take(1));
  }

  public getEventosPorTema(_tema: string): Observable<Evento[]> {
    return this.#http
      .get<Evento[]>(`${this.baseUrl}/${_tema}/tema`)
      .pipe(take(1));
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
