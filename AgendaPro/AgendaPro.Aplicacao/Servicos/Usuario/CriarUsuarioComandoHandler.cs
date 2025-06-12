using AgendaPro.Dominio.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgendaPro.Dominio.Entidades;

namespace AgendaPro.Aplicacao.Servicos
{
    public class CriarUsuarioComandoHandler : IRequestHandler<CriarUsuarioComando, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUsuarioRepositorio _usuarioRepository;

        public CriarUsuarioComandoHandler(IUnitOfWork unitOfWork, IUsuarioRepositorio usuarioRepository)
        {
            _unitOfWork = unitOfWork;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Guid> Handle(CriarUsuarioComando request, CancellationToken cancellationToken)
        {
            var usuario = new Dominio.Entidades.Usuario
            {
                Id = Guid.NewGuid(),
                Nome = request.Nome,
                Email = request.Email
            };

            await _usuarioRepository.AdicionarAsync(usuario);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return usuario.Id;
        }
    }
}
