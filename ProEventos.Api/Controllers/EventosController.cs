using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEvento.Aplicacao.Dto;
using ProEvento.Aplicacao.Interfaces.Servicos;
using ProEventos.Domain.Models;
using System;
using System.Threading.Tasks;

namespace ProEventos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IAppEvento _appEvento;

        public EventosController(IAppEvento appEvento)
        {
            _appEvento = appEvento;
        }

        [HttpGet]
        [Route("ListarTodosEventos")]
        public async Task<ActionResult<Evento>> GetAllEventosAsync()

        {
            try
            {
                var eventos = await _appEvento.GetAllEventosAsync(true);

                if (eventos == null)
                    return NotFound("Nenhum evento encontrado");

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar eventos: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("ListarEventosPorId/{id}")]
        public async Task<ActionResult> GetEventoByIdAsync(int id)

        {
            try
            {
                var eventos = await _appEvento.GetEventoByIdAsync(id, true);

                if (eventos == null)
                    return NotFound("Nenhum evento encontrado com esse Id");

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar eventos: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("ListarEventosPorTema/{tema}")]
        public async Task<ActionResult> GetAllEventosByTemaAsync(string tema)

        {
            try
            {
                var eventos = await _appEvento.GetAllEventosByTemaAsync(tema, true);

                if (eventos == null || eventos.Length == 0)
                    return NotFound("Nenhum evento encontrado com esse tema.");

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar eventos: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("AdicionarEvento")]
        public ActionResult<EventoResponse> AdicionarEvento(Evento evento)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                _appEvento.Add(evento);

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                     $"Erro ao tentar salvar eventos: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("AtualizarEvento/{id}")]
        public ActionResult Update(EventoRequest evento, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                _appEvento.AtualizarEvento(evento, id);

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                     $"Erro ao tentar atualizar eventos: {ex.Message}");
            }
        }

        [HttpDelete]
        public Evento remove()
        {
            return null;
        }
    }
}