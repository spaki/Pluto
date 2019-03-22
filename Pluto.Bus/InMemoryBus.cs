using MediatR;
using Pluto.Domain.Bus;
using Pluto.Domain.Commands.Common;
using Pluto.Domain.Events.Common;
using Pluto.Domain.Interfaces.Repositories;
using Pluto.Domain.Notifications;
using System.Threading.Tasks;

namespace Pluto.Bus
{
    public sealed class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator mediator;
        private readonly IStoredEventRepository storedEventRepository;

        public InMemoryBus(IMediator mediator, IStoredEventRepository storedEventRepository)
        {
            this.mediator = mediator;
            this.storedEventRepository = storedEventRepository;
        }

        public async Task SendAsync<T>(T command) where T : Command => await mediator.Send(command);

        public async Task<TResult> RequestAsync<TResult>(RequestCommand<TResult> command) => await mediator.Send<TResult>(command);

        public Task InvokeAsync<T>(T @event) where T : Event
        {
            if (!@event.MessageType.Equals("DomainNotification"))
                storedEventRepository.AddEventAsync(@event);

            return mediator.Publish(@event);
        }

        public async Task InvokeDomainNotificationAsync(string message) => await mediator.Publish(new DomainNotification(message));
    }
}
