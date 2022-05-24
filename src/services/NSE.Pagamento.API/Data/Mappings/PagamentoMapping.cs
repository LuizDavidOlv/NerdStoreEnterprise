using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NSE.Core.DomainObjects;

namespace NSE.Pagamento.API.Data.Mappings
{
    public class PagamentoMapping : IEntityTypeConfiguration<Models.Pagamento>
    {
        public void Configure(EntityTypeBuilder<Models.Pagamento> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Ignore(c => c.CartaoCredito);

            //1:N Pagamento : Transacoes
            builder.HasMany(c => c.Transacoes)
                .WithOne(c => c.Pagamento)
                .HasForeignKey(c => c.PagamentoId);

            builder.ToTable("Pagamentos");
        }
    }
}
