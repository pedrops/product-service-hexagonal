using MediatR;
using ProductService.Domain.Entities;

namespace ProductService.Application.Queries;

public record GetProductDetailsQuery(int ProductId, int SKU) : IRequest<Product?>;