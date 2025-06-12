using AgendaPro.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaPro.Dominio.Interfaces
{
    public interface IReservaRepositorio
    {
        Task AdicionarAsync(Reserva reserva);
        Task<Reserva?> BuscarPorIdAsync(Guid id);
        Task<bool> VerificarConflitoDeHorarioAsync(Guid salaId, DateTime inicio, DateTime fim, Guid? reservaIdExcluida = null);
        //“Coloquei um reservaIdExcluida opcional pra evitar que a reserva se conflite com ela mesma quando for editada.”
        Task<IEnumerable<Reserva>> ListarTodasAsync();
    }
}
