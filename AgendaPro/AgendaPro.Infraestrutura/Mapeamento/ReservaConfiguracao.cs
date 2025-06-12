using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgendaPro.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgendaPro.Infraestrutura.Persistence.Configurations;

public class ReservaConfiguration : IEntityTypeConfiguration<Reserva>
{
    public void Configure(EntityTypeBuilder<Reserva> builder)
    {
        // Define a chave primária da tabela Reservas
        builder.HasKey(r => r.Id);

        // Define que algumas colunas não podem ser nulas
        builder.Property(r => r.Status).IsRequired();
        builder.Property(r => r.DataHoraInicio).IsRequired();
        builder.Property(r => r.DataHoraFim).IsRequired();

        // Configura o relacionamento com a tabela Salas
        builder.HasOne(reserva => reserva.Sala) // Uma Reserva tem uma Sala
            .WithMany() // Uma Sala pode ter muitas Reservas
            .HasForeignKey(reserva => reserva.IdSala) // A chave estrangeira é IdSala
            .OnDelete(DeleteBehavior.Restrict); // Impede apagar uma Sala se ela tiver Reservas

        // Configura o relacionamento com a tabela Usuarios
        builder.HasOne(reserva => reserva.Usuario) // Uma Reserva tem um Usuario
            .WithMany() // Um Usuario pode ter muitas Reservas
            .HasForeignKey(reserva => reserva.IdUsuario) // A chave estrangeira é IdUsuario
            .OnDelete(DeleteBehavior.Restrict); // Impede apagar um Usuario se ele tiver Reservas
    }
}