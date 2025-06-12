using AgendaPro.Dominio.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaPro.Aplicacao.Servicos.Usuario
{
    public class ExcluirUsuarioComandoHandler : IRequestHandler<ExcluirUsuarioCamando, Unit>
    {
        private readonly IUsuarioRepositorio _usuarioRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ExcluirUsuarioComandoHandler(IUsuarioRepositorio ususarioRepository, IUnitOfWork unitOfWork)
        {
            _usuarioRepository = ususarioRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(ExcluirUsuarioCamando request, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.BuscarPorIdAsync(request.Id);
            if (usuario is null) throw new Exception("Sala não encontrada.");

            _usuarioRepository.Remover(usuario);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
