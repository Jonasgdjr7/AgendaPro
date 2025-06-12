using AgendaPro.API.Dtos;
using AgendaPro.Aplicacao.Servicos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AgendaPro.API.Controller;

[ApiController]
[Route("api/[controller]")] // Define a rota base como /api/reservas
public class ReservasController : ControllerBase
{
    private readonly IMediator _mediator;

    // Injetamos o MediatR. É ele que vai despachar nossas ordens para os Handlers.
    public ReservasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CriarReserva([FromBody] CriarReservaRequest request)
    {
        try
        {
            // Mapeamos o objeto da requisição (DTO) para o nosso Comando da camada de Aplicação.
            var command = new CriarReservaComando(
                request.SalaId,
                request.UsuarioId,
                request.DataHoraInicio,
                request.DataHoraFim);

            // Enviamos o comando para o MediatR. Ele encontrará o Handler correto e o executará.
            var reservaId = await _mediator.Send(command);

            // Se tudo deu certo, retornamos um status 201 Created,
            // indicando a URL para acessar o novo recurso.
            //return CreatedAtAction(nameof(GetById), new { id = reservaId }, request);
            return StatusCode(201, new { id = reservaId });

        }
        catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
        {
            // Capturamos exceções de regras de negócio (lançadas pelo nosso Handler)
            // e retornamos um erro 400 Bad Request com uma mensagem amigável.
            return BadRequest(new { error = ex.Message });
        }
    }

    //Metodo busca por reserva especifica -- ID
    [HttpGet("RetornarReserva/{id}")]
    public IActionResult GetById(Guid id)
    {
        return Ok($"Endpoint para buscar reserva com ID: {id}");
    }
    

    [HttpGet("ListarReservas")]
    [ProducesResponseType(typeof(IEnumerable<Aplicacao.Dtos.ReservaDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListarTodas()
    {
        var query = new ListarReservasComando();
        var resultado = await _mediator.Send(query);
        return Ok(resultado);
    }

    
    [HttpDelete("CancelarReserva/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CancelarReserva(Guid id)
    {
        try
        {
            // Cria o comando com o ID vindo da URL
            var command = new CancelarReservaComando(id);
            // Envia para o Handler correspondente
            await _mediator.Send(command);
            // Retorna a resposta padrão para um DELETE bem-sucedido
            return NoContent();
        }
        catch (Exception ex)
        {
            // Se a regra das 24h falhar ou a reserva não for encontrada,
            // a exceção é capturada aqui.
            return BadRequest(new { error = ex.Message });
        }
    }
}
