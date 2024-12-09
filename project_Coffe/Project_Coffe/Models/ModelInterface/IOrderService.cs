using Project_Coffe.Entities;

namespace Project_Coffe.Models.ModelInterface
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<Order?> GetOrderById(int orderId);
        Task CreateOrder(Order order, List<OrderProduct> orderProducts);
        Task UpdateOrder(Order order, List<OrderProduct> orderProducts);
        Task DeleteOrder(int orderId);
    }

}
