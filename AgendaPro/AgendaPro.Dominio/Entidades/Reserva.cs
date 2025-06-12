using AgendaPro.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaPro.Dominio.Entidades
{
    public class Reserva
    {
        public Guid Id { get; set; }
        public Guid IdSala { get; set; }
        public Guid IdUsuario { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        public StatusReserva Status { get; private set; }

        // Propriedades de navegação para o Entity Framework
        public Sala Sala { get; set; }
        public Usuario Usuario { get; set; }

        // Construtor usado pelo EF Core, não deve ser usado diretamente
        private Reserva() { }

        // Construtor correto para criar uma nova reserva
        public Reserva(Guid salaId, Guid usuarioId, DateTime dataHoraInicio, DateTime dataHoraFim)
        {
            Id = Guid.NewGuid();
            IdSala = salaId;
            IdUsuario = usuarioId;
            DataHoraInicio = dataHoraInicio;
            DataHoraFim = dataHoraFim;
            Status = StatusReserva.Confirmada;
        }

        // Método que encapsula a regra de negócio de cancelamento
        public void Cancelar()
        {
            if (Status == StatusReserva.Cancelada)
            {
                throw new InvalidOperationException("Esta reserva já está cancelada.");
            }

            // REGRA DE NEGÓCIO: Só pode cancelar com 24h de antecedência.
            if ((DataHoraInicio - DateTime.UtcNow).TotalHours < 24)
            {
                throw new InvalidOperationException("A reserva não pode ser cancelada com menos de 24 horas de antecedência.");
            }

            Status = StatusReserva.Cancelada;
        }
    }
}
