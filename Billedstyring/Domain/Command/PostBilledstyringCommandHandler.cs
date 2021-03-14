using System.Threading;
using System.Threading.Tasks;
using Billedstyring.Domain.CommandHandlers;
using Billedstyring.Domain.Events;
using MediatR;
using RabbitMQ.Bus.Bus.Interfaces;

namespace Billedstyring.Domain.Command
{
    public class PostBilledstyringCommandHandler : IRequestHandler<CreatePostBilledstyringCommand, bool>
    {
        private readonly IEventBus eventBus;

        public PostBilledstyringCommandHandler(IEventBus eventBus)
        {
            this.eventBus = eventBus;
        }
        public Task<bool> Handle(CreatePostBilledstyringCommand request, CancellationToken cancellationToken)
        {
            eventBus.PublishEvent(new PostOrderCreatedEvent(request.Id, request.Name, request.Categories));
            return Task.FromResult(true);
        }
    }
}
