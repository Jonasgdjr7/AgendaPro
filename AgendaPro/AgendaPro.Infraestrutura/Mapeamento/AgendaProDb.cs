using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgendaPro.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AgendaPro.Infraestrutura.Persistence;

public class AgendaProDbContext : DbContext
{
    public AgendaProDbContext(DbContextOptions<AgendaProDbContext> options) : base(options)
    {
    }

    // Cada DbSet representa uma tabela que será criada no banco de dados.
    public DbSet<Reserva> Reservas { get; set; }
    public DbSet<Sala> Salas { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Este é um truque de senior: em vez de configurar cada entidade aqui,
        // ele automaticamente procura e aplica todas as configurações que
        // estão em arquivos separados neste mesmo projeto. Mantém tudo limpo!
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
