using ProductService.Domain.Repositories;
using ProductService.Infrastructure.Repositories;

namespace ProductService.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IProductRepository? _productRepository;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IProductRepository ProductRepository
        => _productRepository ??= new ProductRepository(_context);

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}
