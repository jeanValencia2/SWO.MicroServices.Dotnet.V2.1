using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Microservices.Carts.Application.Common.Interfaces;
using SWO.Microservices.Dotnet.Shared.Databases.MongoDB;
using Microservices.Carts.Domain.Aggregates;

namespace Microservices.Carts.Infrastructure.Data;

public class OrderRepository : IOrderRepository
{
    private readonly MongoClient _mongoClient;
    private string CollectionName = "Orders";
    private readonly IMongoDatabase _mongoDatabase;

    public OrderRepository(IMongoDbConnectionProvider mongoDbConnectionProvider, IOptions<DatabaseConfiguration> databaseConfiguration)
    {
        _mongoClient = new MongoClient(mongoDbConnectionProvider.GetMongoUrl());
        _mongoDatabase = _mongoClient.GetDatabase(databaseConfiguration.Value.DatabaseName);
    }

    public async Task<Order> GetAsync(Guid id)
    {
        IMongoCollection<Order> collection = _mongoDatabase.GetCollection<Order>(CollectionName);
                
        var filter = Builders<Order>.Filter.Eq(o => o.Id, id);
        var order = await collection.Find(filter).FirstOrDefaultAsync();

        return order;
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        IMongoCollection<Order> collection = _mongoDatabase.GetCollection<Order>(CollectionName);
        var orders = await collection.Find(_ => true).ToListAsync();

        return orders;
    }

    public Task<Order> AddAsync(Order entity)
    {
        IMongoCollection<Order> collection = _mongoDatabase.GetCollection<Order>(CollectionName);
        collection.InsertOneAsync(entity);

        return Task.FromResult(entity);
    }

    public Task<bool> DeleteAsync(Order entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Order> UpdateAsync(Order entity)
    {
        IMongoCollection<Order> collection = _mongoDatabase.GetCollection<Order>(CollectionName);

        // Asegúrate de tener una propiedad Id en la clase Order
        var filter = Builders<Order>.Filter.Eq(e => e.Id, entity.Id);
        var result = await collection.ReplaceOneAsync(filter, entity);

        if (result.IsAcknowledged && result.ModifiedCount > 0)
        {
            return entity;
        }

        throw new Exception("La actualización no se pudo completar.");
    }
}
