using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
namespace Talabat.Repository.Data.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x=>x.Description).IsRequired();
            builder.Property(x=>x.PictureUrl).IsRequired();
            builder.Property(x=>x.Price).IsRequired().HasColumnType("decimal(18,2)");
            builder.HasOne(p => p.Brand).WithMany(b => b.products).HasForeignKey(f=>f.BrandId);
            builder.HasOne(p => p.Category).WithMany().HasForeignKey(p=>p.CategoryId);
        }
    }
}
