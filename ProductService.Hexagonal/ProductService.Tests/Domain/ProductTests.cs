using ProductService.Domain.Entities;

namespace ProductService.Tests.Domain
{
    public class ProductTests
    {
        [Fact]
        public void Product_Should_Set_Currency_To_Usd_By_Default()
        {
            var product = new Product();
            Assert.Equal("USD", product.Currency);
        }
    }
}
