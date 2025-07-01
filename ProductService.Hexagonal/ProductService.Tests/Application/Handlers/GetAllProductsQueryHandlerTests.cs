using Moq;
using ProductService.Application.Handlers;
using ProductService.Application.Queries;
using ProductService.Domain.Entities;
using ProductService.Domain.Repositories;

namespace ProductService.Tests.Application.Handlers
{
    public class GetAllProductsQueryHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Return_All_Products()
        {
            var expected = new List<Product> { new() { Name = "P1" }, new() { Name = "P2" } };
            var repoMock = new Mock<IProductRepository>();
            repoMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(expected);

            var handler = new GetAllProductsQueryHandler(repoMock.Object);
            var result = await handler.Handle(new GetAllProductsQuery(), CancellationToken.None);

            Assert.Equal(expected.Count, result.Count());
        }
    }
}
