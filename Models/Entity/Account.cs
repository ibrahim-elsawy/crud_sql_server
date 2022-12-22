namespace crud_sql_server.Models.Entity
{
    public class Account
    {
        public int AccountId { get; set; } 
        public string Name {get; set;}
        public virtual ICollection<ApplicationUser> Users{ get; } = new List<ApplicationUser>();
        public virtual ICollection<Order> Orders { get; } = new List<Order>();
	}
}