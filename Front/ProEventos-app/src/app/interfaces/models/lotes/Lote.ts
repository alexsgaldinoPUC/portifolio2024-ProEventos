import { Evento } from "../eventos/Evento";

export interface Lote {
  id?: number;
  nome?: string;
  preco?: number;
  dataInicioLote?: Date;
  dataFimLote?: Date;
  quantidade?: number;
  eventoId?: number;
  evento?: Evento;
}
