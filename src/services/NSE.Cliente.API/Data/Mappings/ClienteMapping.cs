using Microsoft.EntityFrameworkCore;
using NSE.Core.DomainObjects;
using System;

namespace NSE.Cliente.API.Data.Mappings
{
    public class ClienteMapping : IEntityTypeConfiguration<NSE.Cliente.API.Models.Cliente>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<NSE.Cliente.API.Models.Cliente> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.OwnsOne(c => c.Cpf, tf =>
             {
                 tf.Property(c => c.Numero)
                 .IsRequired()
                 .HasMaxLength(Cpf.CpfMaxLength)
                 .HasColumnName("Cpf")
                 .HasColumnType($"varchar({Cpf.CpfMaxLength})");
             });

            builder.OwnsOne(c => c.Email, tf =>
             {
                 tf.Property(c => c.Endereco)
                 .IsRequired()
                 .HasColumnName("Email")
                 .HasColumnType($"varchar({Email.EnderecoMaxLength})");
             });

            builder.HasOne(c => c.Endereco)
                .WithOne(c => c.Cliente);

            builder.ToTable("Clientes");

        }
    }
}
