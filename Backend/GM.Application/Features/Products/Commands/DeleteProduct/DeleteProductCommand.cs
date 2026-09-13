using MediatR;

namespace GM.Application.Features.Products.Commands.DeleteProduct;

public record DeleteProductCommand(int Id) : IRequest;