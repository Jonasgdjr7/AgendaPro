using AgendaPro.Dominio.Entidades;
using AgendaPro.Dominio.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaPro.Aplicacao.Servicos
{
    internal class CriarReservaComandoHandler : IRequestHandler<CriarReservaComando, Guid>
    {
        // Injetamos os "contratos" do Domínio. Não sabemos a implementação, só o que eles fazem.
        private readonly IReservaRepositorio _reservaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CriarReservaComandoHandler(IReservaRepositorio reservaRepository, IUnitOfWork unitOfWork)
        {
            _reservaRepository = reservaRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CriarReservaComando request, CancellationToken cancellationToken)
        {
            // 1. Validações iniciais (aqui podem entrar mais regras)
            if (request.DataHoraFim <= request.DataHoraInicio)
            {
                throw new ArgumentException("A data final da reserva deve ser posterior à data inicial.");
            }

            // 2. REGRA DE NEGÓCIO: Chama o repositório para verificar conflitos de horário.
            var temConflito = await _reservaRepository.VerificarConflitoDeHorarioAsync(
                request.SalaId, request.DataHoraInicio, request.DataHoraFim);

            if (temConflito)
            {
                throw new InvalidOperationException("Conflito de horário. Já existe uma reserva para esta sala no período solicitado.");
            }

            // 3. Se não há conflitos, cria a entidade de Domínio.
            var novaReserva = new Reserva(
                request.SalaId,
                request.UsuarioId,
                request.DataHoraInicio,
                request.DataHoraFim);

            // 4. Usa o repositório para adicionar a nova reserva ao contexto da transação.
            await _reservaRepository.AdicionarAsync(novaReserva);

            // 5. Usa o Unit of Work para "comitar" a transação e salvar tudo no banco de uma vez.
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // 6. (Futuro) Aqui dispararíamos o evento para enviar o e-mail de confirmação.

            // 7. Retorna o ID da reserva que acabamos de criar.
            return novaReserva.Id;
        }

    }
}
