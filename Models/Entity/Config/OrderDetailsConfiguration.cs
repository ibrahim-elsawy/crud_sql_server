using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace crud_sql_server.Models.Entity.Config
{
	public class OrderDetailsConfiguration : IEntityTypeConfiguration<OrderDetails>
	{
		public void Configure(EntityTypeBuilder<OrderDetails> builder)
		{
			builder.HasKey(e => e.OD_Id).HasName("OrderDetails_pkey");

			builder.Property(e => e.ProductName)
                .IsRequired();
            
            builder.Property(e => e.Price)
                .IsRequired();
            
            builder.Property(e => e.Quantity)
                .IsRequired();
            
            builder.Property(e => e.TotalPrice)
                .IsRequired();

			builder.HasOne(e => e.Order) 
                .WithMany(p => p.OrderDetails) 
                .HasForeignKey(e => e.OrderId) 
                .OnDelete(DeleteBehavior.Cascade);

		}
	}
}