import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../../assets/environments/environment';
import { Observable } from 'rxjs';
import { Evento } from '../../interfaces/models/eventos/Evento';

@Injectable()
export class EventoService {
  #http = inject(HttpClient);

  public baseUrl = `${environment.apiURL}eventos`

  public getEventos(): Observable<Evento[]> {
    return this.#http.get<Evento[]>(this.baseUrl);
  }

  public getEventosPorTema(tema: string): Observable<Evento[]> {
    return this.#http.get<Evento[]>(`${this.baseUrl}/${tema}/tema`);
  }

  public getEventoPorId(id: number): Observable<Evento> {
    return this.#http.get<Evento>(`${this.baseUrl}/${id}`);
  }
}
