using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entities;
using ProductService.Infrastructure.Data;
using ProductService.Infrastructure.Repositories;

namespace ProductService.Tests.Infrastructure
{
    public class ProductRepositoryTests
    {
        private readonly AppDbContext _context;
        private readonly ProductRepository _repository;

        public ProductRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;
            _context = new AppDbContext(options);
            _repository = new ProductRepository(_context);
        }        

        [Fact]
        public async Task AddAsync_Should_Persist_Product()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);
            var repository = new ProductRepository(context);

            var product = new Product { Name = "InMemory Test", Price = 50, SKU = 1, Currency = "USD" };
            await repository.AddAsync(product, CancellationToken.None);
            await context.SaveChangesAsync();

            var saved = await context.Products.FirstOrDefaultAsync(p => p.Name == "InMemory Test");
            Assert.NotNull(saved);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Product()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);
            var product = new Product { Name = "ById", Price = 100, SKU = 5, Currency = "USD" };
            context.Products.Add(product);
            await context.SaveChangesAsync();

            var repository = new ProductRepository(context);
            var result = await repository.GetByIdAsync(product.Id, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal("ById", result.Name);
        }

        [Fact]
        public async Task GetBySKUAsync_Should_Return_Product()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);
            var product = new Product { Name = "SKU-Test", Price = 30, SKU = 99, Currency = "USD" };
            context.Products.Add(product);
            await context.SaveChangesAsync();

            var repository = new ProductRepository(context);
            var result = await repository.GetBySKUAsync(99, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(99, result.SKU);
        }

        [Fact]
        public async Task DeleteAsync_Should_Remove_Product()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);
            var product = new Product { Name = "ToDelete", Price = 10, SKU = 8, Currency = "USD" };
            context.Products.Add(product);
            await context.SaveChangesAsync();

            var repository = new ProductRepository(context);
            await repository.DeleteAsync(product.Id, CancellationToken.None);
            await context.SaveChangesAsync();

            var deleted = await context.Products.FindAsync(product.Id);
            Assert.Null(deleted);
        }
    }
}
