using AgendaPro.Dominio.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaPro.Aplicacao.Servicos
{
 
    public class EditarUsuarioComandoHandler : IRequestHandler<EditarUsuarioComando, Unit>
    {
        private readonly IUsuarioRepositorio _usuarioRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EditarUsuarioComandoHandler(IUsuarioRepositorio usuarioRepository, IUnitOfWork unitOfWork)
        {
            _usuarioRepository = usuarioRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(EditarUsuarioComando request, CancellationToken cancellationToken)
        {
            var usuarioExistente = await _usuarioRepository.BuscarPorIdAsync(request.Id);
            if (usuarioExistente is null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            usuarioExistente.Nome = request.Nome;
            usuarioExistente.Email = request.Email;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

}
