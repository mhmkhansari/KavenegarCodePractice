using MediatR;

namespace Kavehnegar.Shared.Framework.Application;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
}