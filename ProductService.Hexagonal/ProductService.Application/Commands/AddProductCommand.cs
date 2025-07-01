using MediatR;
using ProductService.Domain.Entities;

namespace ProductService.Application.Commands;

public record AddProductCommand(Product Product) : IRequest<Product>;
