using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartInvoice.Domain.Entities;

namespace SmartInvoice.Infrastructure.Persistence.config
{
    public class ClientConfig : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
                builder.HasKey(x => x.Id);

                builder.Property(x => x.Name)
                        .HasMaxLength(100)
                        .IsRequired();

                builder.Property(x => x.Email)
                        .IsRequired();

                builder.Property(x => x.Phone)
                        .HasColumnType("varchar(12)")
                        .HasMaxLength(12)
                        .IsRequired();

                builder.Property(x => x.Address)
                        .HasMaxLength(200)
                        .IsRequired(false);

                builder.Property(x => x.CreatedAt)
                        .IsRequired();

                builder.HasMany(x => x.Invoices)
                        .WithOne(x => x.Client)
                        .HasForeignKey(x => x.ClientId)
                        .OnDelete(DeleteBehavior.NoAction);;
        }
    }
}