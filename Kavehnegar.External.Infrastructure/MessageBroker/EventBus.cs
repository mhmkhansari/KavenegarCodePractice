using Kavehnegar.Shared.Framework.Application;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kavehnegar.External.Infrastructure.MessageBroker
{
    public sealed class EventBus : IEventBus
    {
        private readonly IPublishEndpoint _publishEndpoint;
        public EventBus(IPublishEndpoint publishEndpoint) 
        {
            _publishEndpoint = publishEndpoint;
        }
        public Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class 
            => _publishEndpoint.Publish(message, cancellationToken);

    }
}
