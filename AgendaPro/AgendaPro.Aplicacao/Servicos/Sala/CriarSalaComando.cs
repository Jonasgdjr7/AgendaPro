
using MediatR;

namespace AgendaPro.Aplicacao.Servicos
{
    public record CriarSalaComando( 

        string Nome,
        int Capacidade
        ) : IRequest<Guid>;
}