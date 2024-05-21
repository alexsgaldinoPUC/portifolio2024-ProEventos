import { Evento } from "../eventos/Evento";
import { Palestrante } from "./Palestrante";

export interface PalestranteEvento {
  palestranteId: number;
  palestrante?: Palestrante;
  eventoId: number;
  evento?: Evento
}
