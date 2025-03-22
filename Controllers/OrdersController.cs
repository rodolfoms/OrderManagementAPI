using Microsoft.AspNetCore.Mvc;
using OrderManagementAPI.Model;
using OrderManagementAPI.Service;

namespace OrderManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly MongoDBService _mongoDBService;
        private readonly RabbitMQService _rabbitMQService;

        public OrdersController(MongoDBService mongoDBService, RabbitMQService rabbitMQService)
        {
            _mongoDBService = mongoDBService;
            _rabbitMQService = rabbitMQService;
        }

        [HttpGet]
        public async Task<List<Order>> Get() => await _mongoDBService.GetOrdersAsync();

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Order order)
        {
            // Salva o pedido no MongoDB
            await _mongoDBService.CreateOrderAsync(order);

            // Publica no RabbitMQ
            await _rabbitMQService.ConsumeOrderCreatedEventAsync(order);
            
            return CreatedAtAction(nameof(Get), new { id = order.Id }, order);
        }
    }
}
