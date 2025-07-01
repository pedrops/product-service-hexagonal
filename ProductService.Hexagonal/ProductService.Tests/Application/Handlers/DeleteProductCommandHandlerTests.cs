using Moq;
using ProductService.Application.Commands;
using ProductService.Application.Handlers;
using ProductService.Domain.Repositories;

namespace ProductService.Tests.Application.Handlers
{
    public class DeleteProductCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Delete_Product_And_Save()
        {
            var repoMock = new Mock<IProductRepository>();
            var uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(x => x.ProductRepository).Returns(repoMock.Object);

            var handler = new DeleteProductCommandHandler(uowMock.Object);
            await handler.Handle(new DeleteProductCommand(1), CancellationToken.None);

            repoMock.Verify(r => r.DeleteAsync(1, It.IsAny<CancellationToken>()), Times.Once);
            uowMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
