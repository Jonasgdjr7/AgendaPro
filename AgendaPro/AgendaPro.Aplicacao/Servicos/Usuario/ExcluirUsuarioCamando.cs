using AgendaPro.Aplicacao.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaPro.Aplicacao.Servicos.Usuario
{
    public record ExcluirUsuarioCamando(Guid Id) : IRequest<Unit>;
}
