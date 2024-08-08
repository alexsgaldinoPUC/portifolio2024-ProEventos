using ProEventos.Application.Dtos.Lotes;
using ProEventos.Application.Dtos.Palestrantes;
using ProEventos.Application.Dtos.Usuarios;
using ProEventos.Domain.Models.RedesSociais;
using System.ComponentModel.DataAnnotations;

namespace ProEventos.Application.Dtos.Eventos
{
    public class EventoDto
    {
        public int Id { get; set; }
        public string Local { get; set; }
        public DateTime DataEvento { get; set; }

        [//MinLength(3, ErrorMessage ="O campo {0} deve conter no míniom 3 caracteres."),
         //MaxLength(100, ErrorMessage = "O campo {0} deve conter no máximo 100 caracteres.")
         StringLength(50, MinimumLength = 3, ErrorMessage = "O campo {0} deve conter um intervalo entre 4 e 100 caracteres.")]
        public string Tema { get; set; }

        [Display(Name = "Quantidade de Pessoas no Evento"),
         Range(1, 120000, ErrorMessage = "O campo {0} deve estar no intervalo de 1 a 120.000.")]
        public int QtdePessoas { get; set; }

        [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$", ErrorMessage ="Não é uma imagem válida (gif, jpg, jpeg, bmp ou png).")]
        public string ImagemUrl { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório."),
         Phone(ErrorMessage = "O campo {0} está com número inválido.")]
        public string Telefone { get; set; }

        [Display(Name = "e-mail"),
         Required(ErrorMessage = "O campo {0} "),
         EmailAddress(ErrorMessage = "O campo [0} precisa ter uma contúdo válido.")]
        public string Email { get; set; }

        public int UserId { get; set; }
        public UsuarioDto UsuarioDto { get; set; }
        public IEnumerable<LoteDto> Lotes { get; set; }
        public IEnumerable<RedeSocial> RedesSociais { get; set; }
        public IEnumerable<PalestranteEventoDto> PalestrantesEventos { get; set; }
    }
}
