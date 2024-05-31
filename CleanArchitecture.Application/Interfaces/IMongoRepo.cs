using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Domain.Collections;

namespace CleanArchitecture.Application.Interfaces
{
    public interface IMongoRepo
    {
        Task<IEnumerable<Product>> FilterProducts(ProductFilterDto? filter);
    }
}
