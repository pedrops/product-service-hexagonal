using Moq;
using ProductService.Application.Handlers;
using ProductService.Application.Queries;
using ProductService.Domain.Entities;
using ProductService.Domain.Repositories;

namespace ProductService.Tests.Application.Handlers
{
    public class GetProductByIdQueryHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Return_Product_By_Id()
        {
            var expected = new Product { Id = 1, Name = "Item" };
            var repoMock = new Mock<IProductRepository>();
            repoMock.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(expected);

            var handler = new GetProductByIdQueryHandler(repoMock.Object);
            var result = await handler.Handle(new GetProductByIdQuery(1), CancellationToken.None);

            Assert.Equal(expected, result);
        }
    }
}
