using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartInvoice.Domain.Entities;

namespace SmartInvoice.Infrastructure.Persistence.config
{
    public class InvoiceConfig : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.InvoiceNumber)
                    .HasMaxLength(7) //F000001 <-- ejemplo de num. de factura
                    .IsRequired();

            builder.Property(x => x.IssueDate)
                    .IsRequired();

            builder.Property(x => x.DueDate)
                    .IsRequired();

            builder.Property(x => x.Status)
                    .HasConversion<string>()
                    .IsRequired();

            builder.Property(x => x.SubTotal)
                    .HasPrecision(10, 2)
                    .IsRequired();

            builder.Property(x => x.Discount)
                    .HasPrecision(10, 2)
                    .IsRequired();

            builder.Property(x => x.TaxTotal)
                    .HasPrecision(10, 2)
                    .IsRequired();

            builder.Property(x => x.Total)
                    .HasPrecision(10, 2)
                    .IsRequired();

            builder.Property(x => x.CreatedAt)
                    .IsRequired();

            builder.HasMany(x => x.InvoiceItems)
                    .WithOne(x => x.Invoice)
                    .HasForeignKey(x => x.InvoiceId)
                    .OnDelete(DeleteBehavior.NoAction);;

            builder.HasOne(x => x.Client)
                    .WithMany(x => x.Invoices)
                    .HasForeignKey(x => x.ClientId)
                    .OnDelete(DeleteBehavior.NoAction);;
        }
    }
}