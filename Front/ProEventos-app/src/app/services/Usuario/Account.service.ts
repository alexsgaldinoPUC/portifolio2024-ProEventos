import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../assets/environments/environment';
import { map, Observable, ReplaySubject, take } from 'rxjs';
import { Usuario, UsuarioUpdate } from '../../interfaces/models';


@Injectable()
export class AccountService {
  #http = inject(HttpClient);

  private currentUserSource = new ReplaySubject<Usuario>(1);

  public currentUSer$ = this.currentUserSource.asObservable();

  public baseUrl = `${environment.apiURL}Accounts/`;

  public login(_model: any): Observable<void> {
    return this.#http.post<Usuario>(this.baseUrl + '/login', _model)
      .pipe(take(1), map((usuario: Usuario) => {
        if (usuario) {
          this.setCurrentUser(usuario);
        }
      }))
  }

  public setCurrentUser(_usuario: Usuario): void {
    localStorage.setItem('proeventos-api', JSON.stringify(_usuario));

    this.currentUserSource.next(_usuario);
  }

  public logout(): void {
    localStorage.removeItem('proeventos-api');
    this.currentUserSource.next(null!);
    this.currentUserSource.complete();
  }

  public cadastrar(model: any): Observable<void> {
    return this.#http.post<Usuario>(this.baseUrl + '/cadastrarConta', model)
      .pipe(take(1), map((usuario: Usuario) => {
        if (usuario) {
          this.setCurrentUser(usuario);
        }
      }))
  }

  public getUsuario(): Observable<UsuarioUpdate> {
    return this.#http.get<UsuarioUpdate>(this.baseUrl + "getUsuario")
      .pipe(take(1));
  }

  public updateUsuario(_usuarioUpdate: UsuarioUpdate): Observable<void> {
    return this.#http
      .put<UsuarioUpdate>(this.baseUrl+ "AlterarConta", _usuarioUpdate)
      .pipe(take(1), map((usuarioUpdate: UsuarioUpdate) => {
        this.setCurrentUser(usuarioUpdate)
      }))
  }
}
