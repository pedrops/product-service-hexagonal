using MediatR;
using ProductService.Domain.Entities;

namespace ProductService.Application.Commands;

public record UpdateProductCommand(int Id, Product Product) : IRequest;