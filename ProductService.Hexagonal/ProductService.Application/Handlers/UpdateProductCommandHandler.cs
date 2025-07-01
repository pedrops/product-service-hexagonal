using MediatR;
using ProductService.Application.Commands;
using ProductService.Domain.Repositories;

namespace ProductService.Application.Handlers;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        request.Product.Id = request.Id;
        await _unitOfWork.ProductRepository.UpdateAsync(request.Product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}