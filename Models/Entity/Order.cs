
namespace crud_sql_server.Models.Entity
{
    public class Order
    {
        public int OrderId { get; set; }
        public float TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public int AccountId { get; set; }
        public string UserId { get; set; }
        public virtual Account Account { get; set; }
        public virtual ApplicationUser User { get; set; }
		public virtual ICollection<OrderDetails> OrderDetails { get; } = new List<OrderDetails>();
	}
}