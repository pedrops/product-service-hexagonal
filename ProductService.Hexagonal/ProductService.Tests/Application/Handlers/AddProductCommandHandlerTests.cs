using Moq;
using ProductService.Application.Commands;
using ProductService.Domain.Entities;
using ProductService.Domain.Repositories;

namespace ProductService.Tests.Application.Handlers
{
    public class AddProductCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Add_Product_And_Save()
        {
            var product = new Product { Name = "Test", Price = 100 };
            var repoMock = new Mock<IProductRepository>();
            var uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(x => x.ProductRepository).Returns(repoMock.Object);

            var handler = new AddProductCommandHandler(uowMock.Object);
            await handler.Handle(new AddProductCommand(product), CancellationToken.None);

            repoMock.Verify(r => r.AddAsync(product, It.IsAny<CancellationToken>()), Times.Once);
            uowMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}