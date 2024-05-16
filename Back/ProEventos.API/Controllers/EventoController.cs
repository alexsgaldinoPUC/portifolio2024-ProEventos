using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        public EventoController()
        {
        }

        [HttpPost()]
        public string Post()
        {
            return "Exemplo Post";
        }

        [HttpGet()]
        public IEnumerable<Evento> Get()
        {
            return new Evento[]
            {
                new Evento()
                {
                    Id = 1,
                    Tema = "Angular e .Net Core",
                    Local = "Belo Horizonte",
                    Lote = "1� Lote",
                    QtdePessoas = 250,
                    DataEvento = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy")
                },
                new Evento()
                {
                    Id = 2,
                    Tema = "Angular e Suas Novidades",
                    Local = "S�o Paulo",
                    Lote = "2� Lote",
                    QtdePessoas = 350,
                    DataEvento = DateTime.Now.AddDays(3).ToString("dd/MM/yyyy")
                }

            };
        }
    }
}
