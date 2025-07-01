using Moq;
using ProductService.Application.Commands;
using ProductService.Application.Handlers;
using ProductService.Domain.Entities;
using ProductService.Domain.Repositories;

namespace ProductService.Tests.Application.Handlers
{
    public class UpdateProductCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Update_Product_And_Save()
        {
            var product = new Product { Id = 1, Name = "Updated" };
            var repoMock = new Mock<IProductRepository>();
            var uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(x => x.ProductRepository).Returns(repoMock.Object);

            var handler = new UpdateProductCommandHandler(uowMock.Object);
            await handler.Handle(new UpdateProductCommand(1, product), CancellationToken.None);

            repoMock.Verify(r => r.UpdateAsync(product, It.IsAny<CancellationToken>()), Times.Once);
            uowMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
