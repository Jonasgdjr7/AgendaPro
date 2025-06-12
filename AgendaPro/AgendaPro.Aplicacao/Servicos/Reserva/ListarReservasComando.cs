using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgendaPro.Aplicacao.Servicos;
using MediatR;
using System.Collections.Generic;
using AgendaPro.Aplicacao.Dtos;

namespace AgendaPro.Aplicacao.Servicos
{
    public record ListarReservasComando() : IRequest<IEnumerable<ReservaDto>>;
}
