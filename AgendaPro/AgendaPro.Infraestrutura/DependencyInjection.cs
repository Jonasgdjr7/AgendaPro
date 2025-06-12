using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgendaPro.Dominio.Interfaces;
using AgendaPro.Infraestrutura.Persistence;
using AgendaPro.Infraestrutura.Persistence.Repositories;
using AgendaPro.Infraestrutura.Repositorio;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AgendaPro.Infraestrutura;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestruturaServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AgendaProDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IReservaRepositorio, RepositoioReserva>();
        services.AddScoped<ISalaRepositorio, RepositorioSala>();
        services.AddScoped<IUsuarioRepositorio, RepositorioUsuario>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

 
}
