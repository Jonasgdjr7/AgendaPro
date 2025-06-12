using AgendaPro.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaPro.Dominio.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task AdicionarAsync(Usuario usuario);
        Task<Usuario?> BuscarPorIdAsync(Guid id);
        Task<IEnumerable<Usuario>> ListarTodosAsync();
        void Remover(Usuario usuario);
    }
}
