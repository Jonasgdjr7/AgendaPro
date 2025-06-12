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
   
    public class RetornoSalaIdComandoHandler : IRequestHandler<RetornoSalaIdComando, SalaDto?>
    {
        private readonly ISalaRepositorio _salaRepository;

        public RetornoSalaIdComandoHandler(ISalaRepositorio salaRepository)
        {
            _salaRepository = salaRepository;
        }

        public async Task<SalaDto?> Handle(RetornoSalaIdComando request, CancellationToken cancellationToken)
        {
            var sala = await _salaRepository.BuscarPorIdAsync(request.Id);

            if (sala is null)
            {
                return null; // Retorna nulo se a sala não for encontrada
            }

            // Mapeia a entidade para o DTO
            return new SalaDto
            {
                Id = sala.Id,
                Nome = sala.Nome,
                Capacidade = sala.Capacidade
            };
        }
    }
}
