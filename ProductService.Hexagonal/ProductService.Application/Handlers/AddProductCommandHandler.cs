using MediatR;
using ProductService.Application.Commands;
using ProductService.Domain.Entities;
using ProductService.Domain.Repositories;

public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Product>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Product> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.ProductRepository.AddAsync(request.Product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return request.Product;
    }
}