using MediatR;
using StockRich.Application.Command;
using StockRich.Domain.Enum;

namespace StockRich.Application.Handler;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Status>
{
    public Task<Status> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Status.Success);
    }
}