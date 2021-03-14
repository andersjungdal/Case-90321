using System.Threading;
using System.Threading.Tasks;
using Kundestyring.Domain.CommandHandlers;
using Kundestyring.Domain.Events;
using MediatR;
using RabbitMQ.Bus.Bus.Interfaces;

namespace Kundestyring.Domain.Command
{
    public class PostKundestyringCommandHandler : IRequestHandler<CreatePostKundestyringCommand, bool>
    {
        private readonly IEventBus eventBus;

        public PostKundestyringCommandHandler(IEventBus eventBus)
        {
            this.eventBus = eventBus;
        }
        public Task<bool> Handle(CreatePostKundestyringCommand request, CancellationToken cancellationToken)
        {
            eventBus.PublishEvent(new PostKundestyringCreatedEvent(request.Id, request.Name, request.Categories));
            return Task.FromResult(true);
        }
    }
}
