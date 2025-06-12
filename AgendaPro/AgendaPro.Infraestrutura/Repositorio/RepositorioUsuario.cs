using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgendaPro.Dominio.Entidades;
using AgendaPro.Dominio.Interfaces;
using AgendaPro.Infraestrutura.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AgendaPro.Infraestrutura.Repositorio
{
    public class RepositorioUsuario : IUsuarioRepositorio
    {
        private readonly AgendaProDbContext _context;

        public RepositorioUsuario(AgendaProDbContext context)   
        {
            _context = context;
        }

        public async Task AdicionarAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
        }
        public async Task<Usuario?> BuscarPorIdAsync(Guid id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<IEnumerable<Usuario>> ListarTodosAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public void Remover(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
        }
    }
}
