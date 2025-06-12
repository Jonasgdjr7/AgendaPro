using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgendaPro.Dominio.Entidades;
using AgendaPro.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AgendaPro.Infraestrutura.Persistence.Repositories;

public class RepositoioReserva : IReservaRepositorio
{
    private readonly AgendaProDbContext _context;

    public RepositoioReserva(AgendaProDbContext context)        
    {
        _context = context;
    }

    public async Task AdicionarAsync(Reserva reserva)
    {
        await _context.Reservas.AddAsync(reserva);
    }

    public async Task<Reserva?> BuscarPorIdAsync(Guid id)
    {
        return await _context.Reservas.FindAsync(id);
    }

    // Implementação da regra de negócio mais crítica
    public async Task<bool> VerificarConflitoDeHorarioAsync(Guid salaId, DateTime inicio, DateTime fim, Guid? reservaIdExcluida = null)
    {
        // Procura por qualquer reserva que se sobreponha ao intervalo de tempo desejado
        return await _context.Reservas
            .AnyAsync(r =>
                r.Id != reservaIdExcluida &&         // Ignora a própria reserva (útil para edição)
                r.IdSala == salaId &&
                r.Status == Dominio.Enums.StatusReserva.Confirmada &&
                r.DataHoraInicio < fim &&            // Uma reserva existente começa antes do término da nova
                r.DataHoraFim > inicio);             // E termina depois que a nova começa
    }
    public async Task<IEnumerable<Reserva>> ListarTodasAsync()
    {
        // Usamos Include para que o Entity Framework traga também os dados
        // das tabelas relacionadas (Salas e Usuarios) na mesma consulta.
        // Isto evita o problema de N+1 queries.
        return await _context.Reservas
            .Include(r => r.Sala)
            .Include(r => r.Usuario)
            .ToListAsync();
    }
}