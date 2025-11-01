using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartInvoice.Domain.Entities;

namespace SmartInvoice.Infrastructure.Persistence.config
{
    public class InvoiceItemConfig : IEntityTypeConfiguration<InvoiceItem>
    {
        public void Configure(EntityTypeBuilder<InvoiceItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UnitPrice)
                    .HasPrecision(10, 2)
                    .IsRequired();

            builder.Property(x => x.Quantity)
                    .IsRequired();

            builder.Property(x => x.TaxRate)
                    .HasPrecision(10, 2)
                    .IsRequired();

            builder.Property(x => x.Total)
                    .HasPrecision(10, 2)
                    .IsRequired();

            builder.HasOne(x => x.Product)
                    .WithMany()
                    .HasForeignKey(x => x.ProductId)
                    .OnDelete(DeleteBehavior.NoAction);;

            builder.HasOne(x => x.Invoice)
                    .WithMany(x => x.InvoiceItems)
                    .HasForeignKey(x => x.InvoiceId)
                    .OnDelete(DeleteBehavior.NoAction);;
        }
    }
}