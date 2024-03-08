using FluentAssertions;
using MediatR;
using StockRich.Application.Command;
using StockRich.Application.Handler;
using StockRich.Domain.Enum;

namespace StockRich.API.Tests.RegisterTests;

public class RegisterTests
{
    [TestCase(Status.Success)]
    public async Task RegisterUserCommand_Tests(Status expected)
    {
        var handler = new RegisterUserHandler();
        var command = new RegisterUserCommand();
        var actual = await handler.Handle(command, CancellationToken.None);
        actual.Should().Be(expected);
    }
}