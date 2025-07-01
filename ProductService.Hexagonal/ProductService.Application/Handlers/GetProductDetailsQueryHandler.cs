using MediatR;
using ProductService.Application.Queries;
using ProductService.Domain.Entities;
using ProductService.Domain.Repositories;

namespace ProductService.Application.Handlers;

public class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, Product?>
{
    private readonly IProductRepository _repository;

    public GetProductDetailsQueryHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Product?> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
    {
        if (request.ProductId > 0)
            return await _repository.GetByIdAsync(request.ProductId, cancellationToken);

        if (request.SKU > 0)
            return await _repository.GetBySKUAsync(request.SKU, cancellationToken);

        throw new ArgumentException("Must specify productId or SKU.");
    }
}