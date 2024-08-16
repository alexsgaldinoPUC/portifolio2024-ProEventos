import { RedeSocial } from "../redesSociais/RedeSocial";
import { UsuarioUpdate } from "../Usuarios";
import { PalestranteEvento } from "./PalestranteEvento";

export interface Palestrante {
  id: number;
  miniCurriculo: string;
  userId: string;
  user: UsuarioUpdate;
  redesSociais?: RedeSocial[];
  palestrantesEventos?: PalestranteEvento[];
}
