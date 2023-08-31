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
        public async Task<ActionResult<EventoResponse>> GetLotesAsync(int eventoId)
        {
            try
            {
                var lotes = await _appLote.GetLoteByEventoIdAsync(eventoId);

                if (lotes == null)
                    return this.StatusCode(204, new { mensagem = "Lote não encontrado." });

                return Ok(lotes);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar lotes: {ex.Message}");
            }
        }

        [HttpPost("{evenoId}")]
        public ActionResult<EventoResponse> SaveLotes(int evenoId, LoteRequest[] loteRequests)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var lote = _appLote.SaveLote(evenoId, loteRequests);

                if (lote == null)
                    return NoContent();

                return Ok(lote);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar salvar lotes: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("deletarLote/{eventoId}/{loteId}")]
        public async Task<IActionResult> Remove(int eventoId, int loteId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var lote = await _appLote.GetLoteByIdsAsync(eventoId, loteId);

                if (lote == null)
                    return NoContent();

                return await _appLote.DeletarLote(lote.EventoId, lote.Id)
                    ? Ok(new { mensagem = "Lote deletado" })
                    : throw new Exception("Ocorreu um problema  não especifico ao tentar deletar lote.");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar deletar lotes: {ex.Message}");
            }
        }
    }
}