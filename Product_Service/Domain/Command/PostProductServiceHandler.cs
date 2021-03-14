using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Product_Service.Domain.CommandHandlers;
using Product_Service.Domain.Events;
using RabbitMQ.Bus.Bus.Interfaces;

namespace Product_Service.Domain.Command
{
    public class PostProductServiceHandler : IRequestHandler<CreatePostProductServiceCommand, bool>
    {
        private readonly IEventBus eventBus;

        public PostProductServiceHandler(IEventBus eventBus)
        {
            this.eventBus = eventBus;
        }
        public Task<bool> Handle(CreatePostProductServiceCommand request, CancellationToken cancellationToken)
        {
            eventBus.PublishEvent(new PostProductServiceCreatedEvent(request.Id, request.Name, request.Categories));
            return Task.FromResult(true);
        }
    }
}
