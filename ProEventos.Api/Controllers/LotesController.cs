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
    public class LotesController : ControllerBase
    {
        private readonly IAppLote _appLote;
        private readonly IAppEvento _appEvento;

        public LotesController(IAppLote appLote, IAppEvento appEvento)
        {
            _appLote = appLote;
            _appEvento = appEvento;
        }

        [HttpGet("{eventoId}")]
        public async Task<ActionResult<EventoResponse>> GetLoteAsync(int eventoId)

        {
            try
            {
                var eventos = await _appEvento.GetEventoByIdAsync(eventoId, true);

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
        [Route("ListarEventosPorTema/{tema}")]
        public async Task<ActionResult> GetAllEventosByTemaAsync(string tema)

        {
            try
            {
                var eventos = await _appLote.GetAllEventosByTemaAsync(tema, true);

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

        [HttpPost("{evenoId}")]
        public ActionResult<EventoResponse> Update(int evenoId, LoteRequest[] loteRequests)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var eventoAdd = _appLote.AtualizarEvento(evenoId, loteRequests);

                if (eventoAdd.Result == null)
                    return BadRequest(new { mensagem = "Não foi possivel atualizar evento." });

                return Ok(new { mensagem = "O evento foi atualizado com sucesso.", status = 200 });
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
            }
        }

        [HttpDelete("{eventoId}/{LoteId}")]
        public ActionResult<LoteResponse> remove(int eventoId, int loteId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var retorno = _appLote.GetLoteByIdsAsync(eventoId, loteId);

                if (retorno!= null)
                    return BadRequest(new { mensagem = "Não foi possivel o lote, tente novamente." });

                return Ok(retorno);

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