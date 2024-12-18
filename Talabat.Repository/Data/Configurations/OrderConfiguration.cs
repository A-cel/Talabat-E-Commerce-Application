using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repository.Data.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShippingAdderss, x => x.WithOwner());
            builder.Property(p => p.Status).HasConversion(os => os.ToString(), os => (OrderState)Enum.Parse(typeof(OrderState), os));
            builder.HasOne(o => o.DeliveryMethod).WithMany().OnDelete(DeleteBehavior.SetNull);
            builder.Property(o => o.Subtotal).HasColumnType("decimal(18,2)");
           
        }
    }
}
