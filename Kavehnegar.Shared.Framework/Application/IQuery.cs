using MediatR;

namespace Kavehnegar.Shared.Framework.Application;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
