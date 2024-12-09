namespace Project_Coffe.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
        public decimal TotalAmount { get; set; }
    }
}
