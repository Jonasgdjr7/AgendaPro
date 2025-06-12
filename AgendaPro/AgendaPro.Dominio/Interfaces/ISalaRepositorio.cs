using AgendaPro.Dominio.Entidades;

namespace AgendaPro.Dominio.Interfaces
{
    public interface ISalaRepositorio   
    {
        Task AdicionarAsync(Sala sala);
        Task<Sala?> BuscarPorIdAsync(Guid id);
        Task<IEnumerable<Sala>> ListarTodasAsync();
        void Remover(Sala sala);
    }

}
