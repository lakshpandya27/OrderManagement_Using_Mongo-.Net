using MongoDB.Driver;
using OrderManagement.Models;

namespace OrderManagement.Handlers.QueryHandlers
{
    public class OrderQueryService
    {
        private readonly IMongoCollection<Order> _ordersCollection;

        public OrderQueryService(IMongoDatabase database)
        {
            _ordersCollection = database.GetCollection<Order>("Orders");
        }

        public List<Order> GetAllOrders()
        {
            return _ordersCollection.Find(order => true).ToList();
        }

        public Order GetOrderById(string id)
        {
            var filter = Builders<Order>.Filter.Eq(order => order.Id, new MongoDB.Bson.ObjectId(id));
            return _ordersCollection.Find(filter).FirstOrDefault();
        }
    }
}
