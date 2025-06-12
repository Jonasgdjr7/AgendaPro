using AgendaPro.Dominio.Entidades;
using AgendaPro.Dominio.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AgendaPro.Aplicacao.Servicos
{
    public class CriarSalaComandoHandler : IRequestHandler<CriarSalaComando, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        // Precisamos de um repositório para Sala
        private readonly ISalaRepositorio _salaRepository;

        public CriarSalaComandoHandler(IUnitOfWork unitOfWork, ISalaRepositorio salaRepository)
        {
            _unitOfWork = unitOfWork;
            _salaRepository = salaRepository;
        }

        public async Task<Guid> Handle(CriarSalaComando request, CancellationToken cancellationToken)
        {
            var sala = new Sala
            {
                Id = Guid.NewGuid(),
                Nome = request.Nome,
                Capacidade = request.Capacidade
            };

            await _salaRepository.AdicionarAsync(sala);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return sala.Id;
        }
    }
}
