using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaPro.Aplicacao.Servicos
{
    public record EditarSalaComando(Guid Id, string Nome, int Capacidade) : IRequest<Unit>;
}
