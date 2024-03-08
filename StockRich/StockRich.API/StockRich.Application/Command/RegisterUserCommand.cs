using MediatR;
using StockRich.Domain.Enum;
using StockRich.Domain.Request;

namespace StockRich.Application.Command;

public class RegisterUserCommand : IRequest<Status>
{
    public RegisterUserRequest Request { get; set; }
}