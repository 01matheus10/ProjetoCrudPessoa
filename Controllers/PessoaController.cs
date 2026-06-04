using Microsoft.AspNetCore.Mvc;
using ProjetoCrudPessoa.DTOs;
using ProjetoCrudPessoa.Services;

namespace ProjetoCrudPessoa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoasController : ControllerBase
    {
        private readonly IPessoaService _service;

        public PessoasController(IPessoaService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar(PessoaCreateDto dto)
        {
            try
            {
                await _service.AdicionarAsync(dto);

                return Created(string.Empty, new
                {
                    mensagem = "Pessoa cadastrada com sucesso."
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    mensagem = ex.Message
                });
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Atualizar(
            int id,
            PessoaPatchDto dto)
        {
            try
            {
                var atualizado = await _service.AtualizarAsync(id, dto);

                if (!atualizado)
                {
                    return NotFound(new
                    {
                        mensagem = "Pessoa não encontrada."
                    });
                }

                return Ok(new
                {
                    mensagem = "Pessoa atualizada com sucesso."
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    mensagem = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var removido = await _service.RemoverAsync(id);

            if (!removido)
            {
                return NotFound(new
                {
                    mensagem = "Pessoa não encontrada."
                });
            }

            return Ok(new
            {
                mensagem = "Pessoa removida com sucesso."
            });
        }

        [HttpGet]
        public async Task<IActionResult> Listar(
        int page = 1,
        int pageSize = 10)
        {
            var resultado = await _service.ListarAsync(page, pageSize);

            return Ok(resultado);
        }
    }
}