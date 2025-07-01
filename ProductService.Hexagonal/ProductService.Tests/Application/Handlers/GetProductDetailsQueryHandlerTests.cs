using Moq;
using ProductService.Application.Handlers;
using ProductService.Application.Queries;
using ProductService.Domain.Entities;
using ProductService.Domain.Repositories;

namespace ProductService.Tests.Application.Handlers
{
    public class GetProductDetailsQueryHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Return_Product_By_Sku()
        {
            var expected = new Product { SKU = 123 };
            var repoMock = new Mock<IProductRepository>();
            repoMock.Setup(r => r.GetBySKUAsync(123, It.IsAny<CancellationToken>())).ReturnsAsync(expected);

            var handler = new GetProductDetailsQueryHandler(repoMock.Object);
            var result = await handler.Handle(new GetProductDetailsQuery(0, 123), CancellationToken.None);

            Assert.Equal(expected.SKU, result?.SKU);
        }

        [Fact]
        public async Task Handle_Should_Throw_When_Invalid_Params()
        {
            var repoMock = new Mock<IProductRepository>();
            var handler = new GetProductDetailsQueryHandler(repoMock.Object);

            await Assert.ThrowsAsync<ArgumentException>(() =>
                handler.Handle(new GetProductDetailsQuery(0, 0), CancellationToken.None));
        }
    }
}
