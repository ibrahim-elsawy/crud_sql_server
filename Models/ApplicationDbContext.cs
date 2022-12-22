using crud_sql_server.Models.Entity;
using crud_sql_server.Models.Entity.Config;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace crud_sql_server.Models
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		public virtual DbSet<OrderDetails> OrderDetails{ get; set; }
		public virtual DbSet<Order> Orders{ get; set; }
		public virtual DbSet<Account> Accounts{get; set;}


		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			new OrderConfiguration().Configure(builder.Entity<Order>());

			new OrderDetailsConfiguration().Configure(builder.Entity<OrderDetails>());

			new ApplicationUserConfiguration().Configure(builder.Entity<ApplicationUser>());

			new AccountConfiguration().Configure(builder.Entity<Account>());

			string ADMIN_ID = "02174cf0-9412-4cfe-afbf-59f706d72cf6"; 
			string ADMIN_ROLE_ID = "341743f0-asd2-42de-afbf-59kmkkmk72cf6";

			builder.Entity<IdentityRole>().HasData( 
				new IdentityRole{  Id=ADMIN_ROLE_ID, Name="Admin", NormalizedName="ADMIN", ConcurrencyStamp=Guid.NewGuid().ToString()},
				new IdentityRole{  Id=Guid.NewGuid().ToString(), Name="Account", NormalizedName="ACCOUNT", ConcurrencyStamp=Guid.NewGuid().ToString()},
				new IdentityRole{  Id=Guid.NewGuid().ToString(), Name="Viewer", NormalizedName="VIEWER", ConcurrencyStamp=Guid.NewGuid().ToString()},
				new IdentityRole{  Id=Guid.NewGuid().ToString(), Name="Creator", NormalizedName="CREATOR", ConcurrencyStamp=Guid.NewGuid().ToString()});

			var admin = new ApplicationUser{ 
				Id = ADMIN_ID, 
				Email = "Admin@gmail.com",
				NormalizedEmail = "ADMIN@GMAIL.COM",
				SecurityStamp = Guid.NewGuid().ToString(), 
				UserName = "admin1",
				NormalizedUserName = "ADMIN1"
				};

			PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>(); 
			admin.PasswordHash = ph.HashPassword(admin, "Admin123@");
			builder.Entity<ApplicationUser>().HasData(admin);

			builder.Entity<IdentityUserRole<string>>().HasData(
				new IdentityUserRole<string>{RoleId=ADMIN_ROLE_ID, UserId=ADMIN_ID}
			);
		}
	}
}
