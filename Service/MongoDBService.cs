using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OrderManagementAPI.Data;
using OrderManagementAPI.Model;

namespace OrderManagementAPI.Service
{
    public class MongoDBService
    {
        private readonly IMongoCollection<Order> _orders;

        public MongoDBService(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _orders = database.GetCollection<Order>("Orders");
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            var orders = await _orders.Find(order => true)
                .ToListAsync();
            return orders;
        }
        public async Task CreateOrderAsync(Order order)
        {
            await _orders.InsertOneAsync(order);
        }
    }
}
