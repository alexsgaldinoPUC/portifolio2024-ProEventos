import { Lote } from "../lotes/Lote";
import { PalestranteEvento } from "../palestrantes/PalestranteEvento";
import { RedeSocial } from "../redesSociais/RedeSocial";

export interface Evento {
  id: number;
  local: string;
  dataEvento: Date;
  tema: string;
  qtdePessoas: number;
  imagemUrl: string;
  telefone: string;
  email: string;
  lotes: Lote[];
  redesSociais: RedeSocial[];
  palestrantesEventos: PalestranteEvento[];
}
