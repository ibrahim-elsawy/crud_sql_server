using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace crud_sql_server.Models.Entity.Config
{
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.HasKey(e => e.OrderId);

			builder.Property(e => e.TotalPrice).IsRequired();
			builder.Property(e => e.OrderDate).IsRequired();

			builder.HasOne(o => o.Account).WithMany(a => a.Orders)
			.HasForeignKey(o=>o.AccountId)
			.OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(o => o.User).WithMany(u => u.Orders)
			.HasForeignKey(o=>o.UserId) 
			// Stores orders even if users in the account left the company or his user account deleted
			.OnDelete(DeleteBehavior.NoAction); 

		}
	}
}