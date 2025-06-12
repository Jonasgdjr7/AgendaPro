using AgendaPro.API.Dtos;
using AgendaPro.Aplicacao.Servicos; 
using AgendaPro.Aplicacao.Servicos.Usuario;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AgendaPro.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsuariosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CriarUsuario([FromBody] CriarUsuarioRequest request)
        {
            var command = new CriarUsuarioComando(request.Nome, request.Email);
            var usuarioId = await _mediator.Send(command);
            return StatusCode(201, new { id = usuarioId });
        }
        [HttpGet]
        public async Task<IActionResult> ListarUsuarios()
        {
            return Ok(await _mediator.Send(new ListarUsuariosComando()));
        }

        [HttpGet("RetornarUsuario/{id}")]
        public async Task<IActionResult> GetUsuarioById(Guid id)
        {
            var comando = new RetornoUsuarioIdComando(id);
            var resultado = await _mediator.Send(comando);

            return resultado is not null ? Ok(resultado) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirUsuario(Guid id)
        {
            try
            {
                await _mediator.Send(new ExcluirUsuarioCamando(id));
                return NoContent();
            }
            catch (Exception ex)
            {
                // Apahamos erros como "Usuário não encontrado" ou "Usuário com reservas"
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarUsuario(Guid id, [FromBody] CriarUsuarioRequest request)
        {
            var command = new EditarUsuarioComando(id, request.Nome, request.Email);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
