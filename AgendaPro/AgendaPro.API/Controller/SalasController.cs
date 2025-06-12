using AgendaPro.API.Dtos;
using AgendaPro.Aplicacao.Servicos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgendaPro.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalasController : ControllerBase
{
    private readonly IMediator _mediator;

    public SalasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CriarSala([FromBody] CriarSalaRequest request)
    {
        var command = new CriarSalaComando(request.Nome, request.Capacidade);
        var salaId = await _mediator.Send(command);
        return StatusCode(201, new { id = salaId });
    }

    [HttpGet] // ListarSalas
    public async Task<IActionResult> ListarSalas()
    {
        var query = new ListarSalasComando();
        var resultado = await _mediator.Send(query);
        return Ok(resultado);
    }

    [HttpGet("RetonarSala/{id}")]
    public async Task<IActionResult> GetSalaById(Guid id)
    {
        var comando = new RetornoSalaIdComando(id);
        var resultado = await _mediator.Send(comando);

        return resultado is not null ? Ok(resultado) : NotFound();
    }

    [HttpDelete("DeletarSala/{id}")]
    public async Task<IActionResult> ExcluirSala(Guid id)
    {
        try
        {
            await _mediator.Send(new ExcluirSalaComando(id));
            return NoContent();
        }
        catch (DbUpdateException ex)
        {
            // Apahamos especificamente o erro do banco de dados
            // e verificamos se a causa foi uma violação de chave estrangeira.
            if (ex.InnerException?.Message.Contains("REFERENCE constraint") == true)
            {
                return BadRequest(new { message = "Esta sala não pode ser excluída pois existem reservas associadas a ela." });
            }
            // Para outros erros de banco de dados
            return BadRequest(new { message = "Ocorreu um erro ao comunicar com o banco de dados." });
        }
        catch (Exception ex)
        {
            // Para todos os outros erros (ex: Sala não encontrada)
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditarSala(Guid id, [FromBody] CriarSalaRequest request)
    {
        var command = new EditarSalaComando(id, request.Nome, request.Capacidade);
        await _mediator.Send(command);
        return NoContent(); // Resposta padrão para um PUT/PATCH bem-sucedido
    }
}
