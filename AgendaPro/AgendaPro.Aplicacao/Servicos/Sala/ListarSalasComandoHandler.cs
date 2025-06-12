using AgendaPro.Aplicacao.Dtos;
using AgendaPro.Dominio.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaPro.Aplicacao.Servicos;

    public class ListarSalasComandoHandler : IRequestHandler<ListarSalasComando, IEnumerable<SalaDto>>
{
        private readonly ISalaRepositorio _salaRepository;
        // Usaremos AutoMapper para simplificar o mapeamento
        public ListarSalasComandoHandler(ISalaRepositorio salaRepository) => _salaRepository = salaRepository;

        public async Task<IEnumerable<SalaDto>> Handle(ListarSalasComando request, CancellationToken cancellationToken)
        {
            var salas = await _salaRepository.ListarTodasAsync();
            // Mapeamento manual por enquanto
            return salas.Select(s => new SalaDto { Id = s.Id, Nome = s.Nome, Capacidade = s.Capacidade });
        }
    }

