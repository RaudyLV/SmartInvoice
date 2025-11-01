using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartInvoice.Domain.Entities;


namespace SmartInvoice.Infrastructure.Persistence.config
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                    .HasMaxLength(50)
                    .IsRequired();

            builder.Property(x => x.Price)
                    .HasPrecision(10, 2)
                    .IsRequired();

            builder.Property(x => x.Description)
                    .HasMaxLength(200)
                    .IsRequired(false);

            builder.Property(x => x.Stock)
                    .IsRequired();

            builder.Property(x => x.TaxRate)
                    .HasPrecision(10, 2)
                    .IsRequired();

            builder.Property(x => x.CreatedAt)
                    .IsRequired();
        }
    }
}