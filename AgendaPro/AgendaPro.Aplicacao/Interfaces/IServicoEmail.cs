using AgendaPro.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaPro.Aplicacao.Interfaces
{
    public interface IEmailService
    {
        Task EnviarEmailConfirmacaoReserva(Reserva reserva);
    }
}
