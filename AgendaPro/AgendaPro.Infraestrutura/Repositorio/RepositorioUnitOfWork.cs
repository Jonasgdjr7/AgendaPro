using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgendaPro.Dominio.Interfaces;

namespace AgendaPro.Infraestrutura.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AgendaProDbContext _context;

    public UnitOfWork(AgendaProDbContext context)
    {
        _context = context;
    }

    // A única responsabilidade é chamar o SaveChanges do DbContext,
    // garantindo que todas as operações sejam salvas de uma vez.
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}
