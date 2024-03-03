using MediatR;

namespace Kavehnegar.Shared.Framework.Application;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
