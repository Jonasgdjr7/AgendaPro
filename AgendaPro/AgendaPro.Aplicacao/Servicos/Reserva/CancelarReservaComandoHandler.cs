using AgendaPro.Dominio.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaPro.Aplicacao.Servicos
{
    internal class CancelarReservaComandoHandler : IRequestHandler<CancelarReservaComando, Unit>
    {
        private readonly IReservaRepositorio _reservaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CancelarReservaComandoHandler(IReservaRepositorio reservaRepository, IUnitOfWork unitOfWork)
        {
            _reservaRepository = reservaRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CancelarReservaComando request, CancellationToken cancellationToken)
        {
            // 1. Encontrar a reserva
            var reserva = await _reservaRepository.BuscarPorIdAsync(request.Id);

            if (reserva is null)
            {
                throw new Exception("Reserva não encontrada.");
            }

            // 2. Chamar o método de negócio da entidade de Domínio
            // A regra das "24 horas" está encapsulada aqui
            reserva.Cancelar();

            // 3. Salvar as alterações
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
