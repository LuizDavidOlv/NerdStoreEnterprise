using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NSE.Pedido.Domain;

namespace NSE.Pedidos.Infra
{
    public class VoucherMapping : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Codigo)
                .IsRequired()
                .HasColumnType("Vouchers");

            builder.ToTable("Voucher");

        }
    }
}
