using MongoDB.Driver;
using OrderManagement.Models;

namespace OrderManagement.Handlers.CommandHandlers
{
    public class OrderCommandService
    {
        private readonly IMongoCollection<Order> _ordersCollection;

        public OrderCommandService(IMongoDatabase database)
        {
            _ordersCollection = database.GetCollection<Order>("Orders");
        }

        public void CreateOrder(Order newOrder)
        {
            _ordersCollection.InsertOne(newOrder);
        }

        public void UpdateOrder(string id, Order updatedOrder)
        {
            updatedOrder.Id = new MongoDB.Bson.ObjectId(id);
            var filter = Builders<Order>.Filter.Eq(order => order.Id, new MongoDB.Bson.ObjectId(id));
            _ordersCollection.ReplaceOne(filter, updatedOrder);
        }

        public void DeleteOrder(string id)
        {
            var filter = Builders<Order>.Filter.Eq(order => order.Id, new MongoDB.Bson.ObjectId(id));
            _ordersCollection.DeleteOne(filter);
        }
    }
}
