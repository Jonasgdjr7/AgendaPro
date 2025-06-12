using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaPro.Aplicacao.Servicos
{
    public record CriarUsuarioComando(
        string Nome, 
        string Email) : IRequest<Guid>;  
}
