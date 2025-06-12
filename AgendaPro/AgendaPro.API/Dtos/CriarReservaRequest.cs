namespace AgendaPro.API.Dtos;

// Este é o formato de dados que esperamos receber no corpo (body)
// da requisição POST para criar uma reserva.
public class CriarReservaRequest
{
    public Guid SalaId { get; set; }
    public Guid UsuarioId { get; set; }
    public DateTime DataHoraInicio { get; set; }
    public DateTime DataHoraFim { get; set; }
}
