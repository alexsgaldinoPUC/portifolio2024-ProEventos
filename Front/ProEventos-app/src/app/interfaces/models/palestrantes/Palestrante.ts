import { RedeSocial } from "../redesSociais/RedeSocial";
import { PalestranteEvento } from "./PalestranteEvento";

export interface Palestrante {
  id: number;
  nome?: string;
  miniCurriculo?: string;
  imagemUrL?: string;
  telefone?: string;
  email?: string;
  redesSociais?: RedeSocial[];
  palestrantesEventos?: PalestranteEvento[];
}
