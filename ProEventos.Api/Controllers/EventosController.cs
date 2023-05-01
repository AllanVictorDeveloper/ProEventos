using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEvento.Aplicacao.Dto;
using ProEvento.Aplicacao.Interfaces.Servicos;
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
        public async Task<ActionResult<EventoResponse>> GetAllEventosAsync()

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
        public ActionResult<EventoResponse> AdicionarEvento(EventoRequest evento)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var retorno = _appEvento.AdicionarEvento(evento);

                if (retorno.Result == null)
                    BadRequest(new { mensagem = "Não foi possivel adicionar evento." });

                return Ok(new { mensagem = "Evento adicionado com sucesso", status = 200 });
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
            }
        }

        [HttpPost]
        [Route("AtualizarEvento/{id}")]
        public ActionResult<EventoResponse> Update(int id, [FromBody] EventoRequest evento)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var eventoAdd = _appEvento.AtualizarEvento(id, evento);

                if (eventoAdd.Result == null)
                    return BadRequest(new { mensagem = "Não foi possivel atualizar evento." });

                return Ok(new { mensagem = "O evento foi atualizado com sucesso.", status = 200 });
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
            }
        }

        [HttpDelete]
        [Route("DeletarEvento/{id}")]
        public ActionResult remove(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var retorno = _appEvento.DeletarEvento(id);

                if (retorno.Result != "Deletado")
                    return BadRequest(new { mensagem = "Não foi possivel excluir o evento, tente novamente." });

                return Ok(new { mensagem = "O evento foi deletado com sucesso.", status = 200 });

                //return this.StatusCode(200, new ObjectResult(new { mensagem = "O evento foi deletado com sucesso" }));
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });

                //return BadRequest(new ObjectResult(new { mensagem = "Não foi possivel deletar o evento.", status = 400 }));
            }
        }
    }
}