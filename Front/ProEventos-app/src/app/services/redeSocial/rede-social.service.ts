import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { environment } from "../../../assets/environments/environment";
import { Observable, take } from "rxjs";
import { RedeSocial } from "../../interfaces/models";

@Injectable()
export class RedeSocialService {
  #http = inject(HttpClient);

  public baseUrl = `${environment.apiURL}redesSociais`;

  public getRedesSociais(_origem: string, _id: number): Observable<RedeSocial[]> {
    let url = _id == 0 ? `${this.baseUrl}/${_origem}` :  `${this.baseUrl}/${_origem}/${_id}`

    return this.#http
      .get<RedeSocial[]>(url)
      .pipe(take(1));
  }

  public saveRedesSociais(_origem: string, _id: number, _redesSociais: RedeSocial[]): Observable<RedeSocial[]> {
    let url = _id == 0 ? `${this.baseUrl}/${_origem}` :  `${this.baseUrl}/${_origem}/${_id}`

    return this.#http
      .put<RedeSocial[]>(url, _redesSociais)
      .pipe(take(1));
  }

  public deleteRedeSocial(_origem: string, _id: number, _redeSocialId: number): Observable<any> {
    let url = _id == 0 ? `${this.baseUrl}/${_origem}/${_redeSocialId}` :  `${this.baseUrl}/${_origem}/${_id}/${_redeSocialId}`

    return this.#http
      .delete(url)
      .pipe(take(1));
  }

}
