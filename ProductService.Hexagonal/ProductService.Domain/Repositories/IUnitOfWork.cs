namespace ProductService.Domain.Repositories;

public interface IUnitOfWork
{
    IProductRepository ProductRepository { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
