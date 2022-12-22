using Microsoft.AspNetCore.Identity;

namespace crud_sql_server.Models.Entity
{
	public class ApplicationUser : IdentityUser
	{
		public int? AccountId { get; set; }
		public virtual Account Account {get; set;}
		public virtual ICollection<Order> Orders { get; } = new List<Order>();
	}
}