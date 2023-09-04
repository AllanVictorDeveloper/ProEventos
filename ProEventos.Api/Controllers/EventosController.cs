using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEvento.Aplicacao.Dto;
using ProEvento.Aplicacao.Interfaces.Servicos;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IAppEvento _appEvento;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EventosController(IAppEvento appEvento, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _appEvento = appEvento;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
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
                var evento = await _appEvento.GetEventoByIdAsync(id, true);

                if (evento == null)
                    return NotFound("Nenhum evento encontrado com esse Id");

                return Ok(evento);
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

                return Ok(retorno.Result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
            }
        }

        [HttpPost]
        [Route("AtualizarEvento/{id}")]
        public ActionResult<EventoResponse> AtualizarEvento(int id, [FromBody] EventoRequest evento)
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
        public async Task<ActionResult> DeletarEvento(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var evento = await _appEvento.GetEventoByIdAsync(id, true);

                this.DeleteImagem(evento.ImagemUrl);

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

        [HttpPost]
        [Route("upload-imagem/{eventoId}")]
        public async Task<ActionResult> UploadImagem(int eventoId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var evento = await _appEvento.GetEventoByIdAsync(eventoId, true);

                if (evento == null)
                    return NoContent();

                var file = Request.Form.Files[0];

                if (file.Length > 0)
                {
                    this.DeleteImagem(evento.ImagemUrl);
                    evento.ImagemUrl = await this.SaveImagem(file);
                }

                var eventoRequest = _mapper.Map<EventoRequest>(evento);

                var EventoRetorno = await _appEvento.AtualizarEvento(eventoId, eventoRequest);

                return Ok(EventoRetorno);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
            }
        }

        [NonAction]
        public async Task<string> SaveImagem(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');

            imageName = $"{imageName}{DateTime.UtcNow.ToString("yymmssfff")}{Path.GetExtension(imageFile.FileName)}";

            var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, @"Resources/images", imageName);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return imageName;
        }

        [NonAction]
        public void DeleteImagem(string imageName)
        {
            var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, @"Resources/images", imageName);

            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }
    }
}