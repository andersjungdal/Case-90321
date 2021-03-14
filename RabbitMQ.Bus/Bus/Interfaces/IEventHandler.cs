using System.Threading.Tasks;

namespace RabbitMQ.Bus.Bus.Interfaces
{
    public interface IEventHandler<in TEvent> : IEventHandler
    {
        Task Handle(TEvent @event);
    }
    public interface IEventHandler
    {
    }
}
