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

    public class RetornoUsuarioIdComandoHandler : IRequestHandler<RetornoUsuarioIdComando, UsuarioDto?>
    {
        private readonly IUsuarioRepositorio _salaRepository;

        public RetornoUsuarioIdComandoHandler(IUsuarioRepositorio salaRepository)
        {
            _salaRepository = salaRepository;
        }

        public async Task<UsuarioDto?> Handle(RetornoUsuarioIdComando request, CancellationToken cancellationToken)
        {
            var usuario = await _salaRepository.BuscarPorIdAsync(request.Id);

            if (usuario is null)
            {
                return null; // Retorna nulo se a sala não for encontrada
            }

            // Mapeia a entidade para o DTO
            return new UsuarioDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
            };
        }
    }
}
