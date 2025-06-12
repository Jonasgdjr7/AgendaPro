using AgendaPro.Dominio.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaPro.Aplicacao.Servicos
{
    public class ExcluirSalaComandoHandler : IRequestHandler<ExcluirSalaComando, Unit>
    {
        private readonly ISalaRepositorio _salaRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ExcluirSalaComandoHandler(ISalaRepositorio salaRepository, IUnitOfWork unitOfWork)
        {
            _salaRepository = salaRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(ExcluirSalaComando request, CancellationToken cancellationToken)
        {
            var sala = await _salaRepository.BuscarPorIdAsync(request.Id);
            if (sala is null) throw new Exception("Sala não encontrada.");

            _salaRepository.Remover(sala);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
