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
    internal class ListarReservasComandoHandler : IRequestHandler<ListarReservasComando, IEnumerable<ReservaDto>>
    {
        private readonly IReservaRepositorio _reservaRepository;

        public ListarReservasComandoHandler(IReservaRepositorio reservaRepository)
        {
            _reservaRepository = reservaRepository;
        }

        public async Task<IEnumerable<ReservaDto>> Handle(ListarReservasComando request, CancellationToken cancellationToken)
        {
            // 1. Buscar todas as reservas do banco de dados
            var reservas = await _reservaRepository.ListarTodasAsync();

            // 2. Mapear as entidades de domínio para os DTOs
            var reservasDto = reservas.Select(r => new ReservaDto
            {
                Id = r.Id,
                NomeSala = r.Sala?.Nome ?? "Sala não encontrada",
                NomeUsuario = r.Usuario?.Nome ?? "Usuário não encontrado",
                DataHoraInicio = r.DataHoraInicio,
                DataHoraFim = r.DataHoraFim,
                Status = r.Status.ToString()
            });

            return reservasDto;
        }
    }
}
