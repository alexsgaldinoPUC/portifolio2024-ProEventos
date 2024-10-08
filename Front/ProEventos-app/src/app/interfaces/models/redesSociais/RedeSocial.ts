import { Evento } from "../eventos/Evento"
import { Palestrante } from "../palestrantes/Palestrante"

export interface RedeSocial {
  id: number
  nome?: string
  url?: string
  eventoId?: number
  evento?: Evento
  palstranteId?: number
  palestrante?: Palestrante
}
