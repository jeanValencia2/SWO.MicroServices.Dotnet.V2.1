using Microservices.Carts.Application.Common.Interfaces;
using Microservices.Carts.Domain.Aggregates;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SWO.Microservices.Dotnet.Shared.Databases.MongoDB;
using SWO.Microservices.Dotnet.Shared.Domain.Entities;

namespace Microservices.Carts.Infrastructure.Data;

public class ProductRepository : IProductRepository
{
    private readonly MongoClient _mongoClient;
    private string CollectionName = "Orders";
    private readonly IMongoDatabase _mongoDatabase;

    public ProductRepository(IMongoDbConnectionProvider mongoDbConnectionProvider, IOptions<DatabaseConfiguration> databaseConfiguration)
    {
        _mongoClient = new MongoClient(mongoDbConnectionProvider.GetMongoUrl());
        _mongoDatabase = _mongoClient.GetDatabase(databaseConfiguration.Value.DatabaseName);
    }

    public Task<Product> UpdateAsync(Product entity)
    {
        IMongoCollection<Order> collection = _mongoDatabase.GetCollection<Order>(CollectionName);

        var filter = Builders<Order>.Filter.ElemMatch(x => x.Products, p => p.Id == entity.Id);
        var update = Builders<Order>.Update
            .Set("Products.$.Name", entity.Name)
            .Set("Products.$.Description", entity.Description);

        var result = collection.UpdateMany(filter, update);

        return Task.FromResult(entity);
    }
}