using MediatR;

namespace CSharpApp.Application.Abstractions.Commands;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}