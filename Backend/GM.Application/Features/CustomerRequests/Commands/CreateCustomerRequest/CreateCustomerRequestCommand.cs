using MediatR;

namespace GM.Application.Features.CustomerRequests.Commands.CreateCustomerRequest;

public record CreateCustomerRequestCommand(
    int UserId,
    int ServiceId,
    string Description,
    DateTime? PreferredDate) : IRequest<int>;
