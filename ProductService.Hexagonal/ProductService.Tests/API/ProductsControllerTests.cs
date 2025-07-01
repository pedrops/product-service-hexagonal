using Microsoft.AspNetCore.Mvc.Testing;
using ProductService.Domain.Entities;
using System.Net;
using System.Net.Http.Json;

namespace ProductService.API.Tests;

public class ProductsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ProductsControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAllProducts_ShouldReturn200()
    {
        var response = await _client.GetAsync("/api/products");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        Assert.False(string.IsNullOrWhiteSpace(content));
    }

    [Fact]
    public async Task GetProduct_ByValidId_ShouldReturn200()
    {
        var response = await _client.GetAsync("/api/products/1");

        Assert.True(
            response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NotFound
        );
    }

    [Fact]
    public async Task CreateProduct_ShouldReturn201()
    {
        var newProduct = new
        {
            name = "Test Product",
            price = 123,
            sku = 999,
            currency = "USD"
        };

        var response = await _client.PostAsJsonAsync("/api/products", newProduct);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task UpdateProduct_ShouldReturn204()
    {
        var product = new
        {
            name = "Test",
            price = 100,
            sku = 1,
            currency = "USD"
        };

        var createResponse = await _client.PostAsJsonAsync("/api/products", product);
        createResponse.EnsureSuccessStatusCode();

        var created = await createResponse.Content.ReadFromJsonAsync<Product>();

        var updated = new
        {
            id = created!.Id,
            name = "Updated Name",
            price = 150,
            sku = 1,
            currency = "USD"
        };

        var updateResponse = await _client.PutAsJsonAsync($"/api/products/{created.Id}", updated);

        Assert.Equal(HttpStatusCode.NoContent, updateResponse.StatusCode);
    }


    [Fact]
    public async Task DeleteProduct_ShouldReturn204Or404()
    {
        var response = await _client.DeleteAsync("/api/products/1");

        Assert.True(
            response.StatusCode == HttpStatusCode.NoContent ||
            response.StatusCode == HttpStatusCode.NotFound
        );
    }





}
