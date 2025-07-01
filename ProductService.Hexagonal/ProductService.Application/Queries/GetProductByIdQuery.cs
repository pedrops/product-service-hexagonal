using MediatR;
using ProductService.Domain.Entities;

namespace ProductService.Application.Queries;

public record GetProductByIdQuery(int Id) : IRequest<Product?>;