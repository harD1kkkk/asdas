using Project_Coffe.Entities;

namespace Project_Coffe.DTO
{
    public class UpdateOrderDTO
    {
        public Order? Order { get; set; }
        public List<OrderProduct>? OrderProducts { get; set; }
    }

}
