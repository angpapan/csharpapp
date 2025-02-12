using MediatR;

namespace CSharpApp.Application.Abstractions.Commands;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
}
