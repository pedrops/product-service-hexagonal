using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entities;
using ProductService.Domain.Repositories;
using ProductService.Infrastructure.Data;

namespace ProductService.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for managing <see cref="Product"/> entities using Entity Framework Core.
/// </summary>
public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Initializes a new instance of the Product class.
    /// </summary>
    /// <param name="context">The application database context.</param>
    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Adds a new product to the database and saves the changes.
    /// </summary>
    /// <param name="product">The product to add.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    public async Task AddAsync(Product product, CancellationToken cancellationToken)
    {
        await _context.Products.AddAsync(product, cancellationToken);
    }

    /// <summary>
    /// Deletes the product with the specified ID, if it exists.
    /// </summary>
    /// <param name="id">The ID of the product to delete.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await _context.Products.FindAsync(new object[] { id }, cancellationToken);
        if (entity is not null)
        {
            _context.Products.Remove(entity);
        }
    }

    /// <summary>
    /// Retrieves all products from the database.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A list of <see cref="Product"/> entities.</returns>
    public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Products.ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Retrieves a product by its unique identifier.
    /// </summary>
    /// <param name="id">The ID of the product.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The <see cref="Product"/> if found; otherwise, null.</returns>
    public async Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Products.FindAsync(new object[] { id }, cancellationToken);
    }

    /// <summary>
    /// Retrieves a product by its SKU value.
    /// </summary>
    /// <param name="sku">The Stock Keeping Unit value.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The matching <see cref="Product"/> if found; otherwise, null.</returns>
    public async Task<Product?> GetBySKUAsync(int sku, CancellationToken cancellationToken)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.SKU == sku, cancellationToken);
    }

    /// <summary>
    /// Updates an existing product and persists the changes.
    /// </summary>
    /// <param name="product">The product with updated values.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    public async Task UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        _context.Products.Update(product);
    }
}
