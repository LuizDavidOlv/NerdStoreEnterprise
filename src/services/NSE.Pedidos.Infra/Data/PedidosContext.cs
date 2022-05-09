using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using NSE.Core.Data;
using NSE.Core.DomainObjects;
using NSE.Core.Mediator;
using NSE.Core.Messages;
using NSE.Pedido.Domain;
using NSE.Pedidos.Domain.Pedidos;

namespace NSE.Pedidos.Infra.Data
{
    public class PedidosContext : DbContext
    {
        private readonly IMediatorHandler _mediatorHandler;

        public PedidosContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        public PedidosContext(DbContextOptions<PedidosContext> options, IMediatorHandler mediatorHandler) : base(options)
        {
            _mediatorHandler = mediatorHandler;
        }


        //public DbSet<Domain.Pedidos.Pedido> Pedidos { get; set; }
        //public DbSet<PedidoItem> PedidoItems { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.Ignore<Event>();
            modelBuilder.Ignore<ValidationResult>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PedidosContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.HasSequence<int>("MinhaSequencia").StartsAt(1000).IncrementsBy(1);

            base.OnModelCreating(modelBuilder);
        }


        //public async Task<bool> Commit()
        //{
        //    foreach (var entry in ChangeTracker.Entries()
        //        .Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
        //    {
        //        if(entry.State == EntityState.Added)
        //        {
        //            entry.Property("DataCadastro").CurrentValue = DateTime.Now;
        //        }
        //        if(entry.State == EntityState.Modified)
        //        {
        //            entry.Property("DataCadastro").IsModified = false;
        //        }
        //    }

        //    var sucesso = await base.SaveChangesAsync() > 0;

        //    if(sucesso)
        //    {
        //        await _mediatorHandler.PublicarEventos(this);
        //    }

        //    return sucesso;
        //}
    }

    public class YourDbContextFactory : IDesignTimeDbContextFactory<PedidosContext>
    {
        public PedidosContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PedidosContext>();
            optionsBuilder.UseSqlServer("your connection string");

            return new PedidosContext(optionsBuilder.Options);
        }

        // PedidosContext IDesignTimeDbContextFactory<PedidosContext>.CreateDbContext(string[] args)
        //{
        //    var optionsBuilder = new DbContextOptionsBuilder<PedidosContext>();
        //    optionsBuilder.UseSqlServer("your connection string");

        //    return new PedidosContext(optionsBuilder.Options);
        //}
    }
    //public static class MediatorExtension
    //{
    //    public static async Task PublicarEventos<T>(this IMediatorHandler mediator, T ctx) where T : DbContext
    //    {
    //        var domainEntities = ctx.ChangeTracker
    //            .Entries<Entity>()
    //            .Where(x => x.Entity.Notificacoes != null && x.Entity.Notificacoes.Any());

    //        var domainEvents = domainEntities
    //            .SelectMany(x => x.Entity.Notificacoes)
    //            .ToList();

    //        domainEntities.ToList()
    //            .ForEach(entity => entity.Entity.LimparEventos());

    //        var tasks = domainEvents
    //            .Select(async (domainEvent) => {
    //                await mediator.PublicarEvento(domainEvent);
    //            });

    //        await Task.WhenAll(tasks);
    //    }
    //}
}
