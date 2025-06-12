using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace AgendaPro.Aplicacao.Servicos
{
    public record CriarReservaComando(  
        Guid SalaId,
        Guid UsuarioId,
        DateTime DataHoraInicio,
        DateTime DataHoraFim) : IRequest<Guid>;
}
