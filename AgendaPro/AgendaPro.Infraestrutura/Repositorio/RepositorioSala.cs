using AgendaPro.Dominio.Entidades;
using AgendaPro.Dominio.Interfaces;
using AgendaPro.Infraestrutura.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AgendaPro.Infraestrutura.Repositorio
{
    internal class RepositorioSala : ISalaRepositorio
    {
        public readonly AgendaProDbContext _context;

        public RepositorioSala(AgendaProDbContext context)  
        {
            _context = context;
        }

        public async Task AdicionarAsync(Sala sala)
        {
            await _context.Salas.AddAsync(sala);
        }
        public async Task<Sala?> BuscarPorIdAsync(Guid id)
        {
            return await _context.Salas.FindAsync(id);
        }

        public async Task<IEnumerable<Sala>> ListarTodasAsync()
        {
            return await _context.Salas.ToListAsync();
        }

        public void Remover(Sala sala)
        {
            _context.Salas.Remove(sala);
        }
    }
}
