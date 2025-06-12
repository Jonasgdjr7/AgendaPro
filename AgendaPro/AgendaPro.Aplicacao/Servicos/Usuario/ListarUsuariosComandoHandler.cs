using AgendaPro.Aplicacao.Dtos;
using AgendaPro.Dominio.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaPro.Aplicacao.Servicos
{
    public class ListarUsuariosComandoHandler : IRequestHandler<ListarUsuariosComando, IEnumerable<UsuarioDto>>
    {
        private readonly IUsuarioRepositorio _usuarioRepository;

        public ListarUsuariosComandoHandler(IUsuarioRepositorio usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEnumerable<UsuarioDto>> Handle(ListarUsuariosComando request, CancellationToken cancellationToken)
        {

            var usuarios = await _usuarioRepository.ListarTodosAsync();
            // Mapeia a entidade de domínio para o DTO de saída
            return usuarios.Select(u => new UsuarioDto
            {
                Id = u.Id,
                Nome = u.Nome,
                Email = u.Email
            });
        }
    }
}
