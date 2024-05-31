using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Collections;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMongoRepo mongoRepo;

        public ProductController(IMongoRepo mongoRepo)
        {
            this.mongoRepo = mongoRepo;
        }

        [HttpPost]
        public async Task<IEnumerable<Product>> Filter(ProductFilterDto? dto) =>
            await mongoRepo.FilterProducts(dto);
    }
}