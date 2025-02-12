using MediatR;

namespace CSharpApp.Application.Abstractions.Queries;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}