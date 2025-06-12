using AgendaPro.Dominio.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaPro.Aplicacao.Servicos
{
    public class EditarSalaComandoHandler : IRequestHandler<EditarSalaComando, Unit>
    {
        private readonly ISalaRepositorio _salaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EditarSalaComandoHandler(ISalaRepositorio salaRepository, IUnitOfWork unitOfWork)
        {
            _salaRepository = salaRepository;
            _unitOfWork = unitOfWork;
        }

        // --- CORREÇÃO AQUI ---
        // O tipo de retorno do método foi alterado para async Task<Unit>
        public async Task<Unit> Handle(EditarSalaComando request, CancellationToken cancellationToken)
        {
            // 1. Encontrar a sala existente no banco
            var salaExistente = await _salaRepository.BuscarPorIdAsync(request.Id);
            if (salaExistente is null)
            {
                throw new Exception("Sala não encontrada.");
            }

            // 2. Atualizar as suas propriedades
            salaExistente.Nome = request.Nome;
            salaExistente.Capacidade = request.Capacidade;

            // 3. Salvar as alterações
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // --- CORREÇÃO AQUI ---
            // Retornamos Unit.Value para sinalizar a conclusão com sucesso.
            return Unit.Value;
        }
    }
}
