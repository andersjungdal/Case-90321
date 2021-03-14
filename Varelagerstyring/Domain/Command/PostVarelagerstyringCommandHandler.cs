using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RabbitMQ.Bus.Bus.Interfaces;
using Varelagerstyring.Domain.CommandHandlers;
using Varelagerstyring.Domain.Events;

namespace Varelagerstyring.Domain.Command
{
    public class PostVarelagerstyringCommandHandler : IRequestHandler<CreatePostVarelagerstyringCommand, bool>
    {
        private readonly IEventBus eventBus;

        public PostVarelagerstyringCommandHandler(IEventBus eventBus)
        {
            this.eventBus = eventBus;
        }
        public Task<bool> Handle(CreatePostVarelagerstyringCommand request, CancellationToken cancellationToken)
        {
            eventBus.PublishEvent(new PostOrderCreatedEvent(request.Id, request.Name, request.Categories));
            return Task.FromResult(true);
        }
    }
}
