using Microsoft.AspNetCore.Mvc;
using Project_Coffe.DTO;
using Project_Coffe.Entities;
using Project_Coffe.Models.ModelInterface;

namespace Project_Coffe.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderService orderService, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                List<Order> orders = (await _orderService.GetAllOrders()).ToList();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching orders: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            try
            {
                Order? order = await _orderService.GetOrderById(id);
                if (order == null)
                {
                    _logger.LogWarning($"Order with ID {id} not found.");
                    return NotFound();
                }
                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching order with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO createOrderDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state for CreateOrder request.");
                    return BadRequest(ModelState);
                }

                Order order = new Order
                {
                    UserId = createOrderDto.UserId,
                    TotalAmount = 0 
                };

                List<OrderProduct> orderProducts = createOrderDto.OrderProducts
                    .Select(op => new OrderProduct
                    {
                        ProductId = op.ProductId,
                        Quantity = op.Quantity
                    })
                    .ToList();

                if (order.UserId == 0)
                {
                    _logger.LogError("UserId is not provided in the order.");
                    return BadRequest("UserId cannot be null.");
                }

                if (orderProducts == null || orderProducts.Count == 0)
                {
                    _logger.LogError("Order products list is null or empty.");
                    return BadRequest("Order products cannot be null or empty.");
                }

                await _orderService.CreateOrder(order, orderProducts);
                return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating order: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] UpdateOrderDTO updateOrderDto)
        {
            try
            {
                if (updateOrderDto.Order == null)
                {
                    _logger.LogWarning("UpdateOrder: Order is null.");
                    return BadRequest("Order cannot be null.");
                }

                if (!ModelState.IsValid || id != updateOrderDto.Order.Id)
                {
                    _logger.LogWarning($"Invalid request data for updating order with ID {id}.");
                    return BadRequest(ModelState);
                }

                if (updateOrderDto.OrderProducts == null)
                {
                    _logger.LogWarning($"Order products list is null for order with ID {id}.");
                    return BadRequest("Order products cannot be null.");
                }

                await _orderService.UpdateOrder(updateOrderDto.Order, updateOrderDto.OrderProducts);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating order with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                await _orderService.DeleteOrder(id);
                _logger.LogInformation($"Order with ID {id} successfully deleted.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting order with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
