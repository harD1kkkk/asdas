using Project_Coffe.Entities;

namespace Project_Coffe.DTO
{
    public class CreateOrderDTO
    {
        public int UserId { get; set; }
        public List<CreateOrderProductDTO>? OrderProducts { get; set; } 
    }

}
