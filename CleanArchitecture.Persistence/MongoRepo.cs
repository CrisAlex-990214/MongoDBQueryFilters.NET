using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Collections;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace CleanArchitecture.Persistence
{
    public class MongoRepo : IMongoRepo
    {
        private readonly IMongoCollection<Product> productCollection;

        public MongoRepo(IConfiguration configuration)
        {
            productCollection = new MongoClient(configuration.GetConnectionString("MongoDB"))
                .GetDatabase("Shop").GetCollection<Product>("Product");
        }

        public async Task<IEnumerable<Product>> FilterProducts(ProductFilterDto? filter)
        {
            var builder = Builders<Product>.Filter;

            var filterDefinition = filter is null ? builder.Empty :

                (string.IsNullOrEmpty(filter.Name) ?
                builder.Empty :
                builder.Regex(f => f.Name, new MongoDB.Bson.BsonRegularExpression(filter.Name, "i"))
                ) &

                (filter.Brands.Any() ?
                builder.StringIn(f => f.Brand, filter.Brands.Select(x => new StringOrRegularExpression(x)))
                : builder.Empty) &

                (filter.Colors.Any() ?
                builder.AnyStringIn(f => f.Colors, filter.Colors.Select(x => new StringOrRegularExpression(x)))
                : builder.Empty) &

                (filter.PriceRange.Count() == 2 ?
                builder.Gte(f => f.Price, filter.PriceRange[0]) & builder.Lte(f => f.Price, filter.PriceRange[1])
                : builder.Empty)
                ;

            return await productCollection.Find(filterDefinition).ToListAsync();
        }
    }
}