using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Bus.Bus.Interfaces;
using RabbitMQ.Bus.Commands;
using RabbitMQ.Bus.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQ.Bus.Bus
{
    //sealed for at undgå arv
    public sealed class RabbitMQBus : IEventBus
    {
        private readonly IMediator _mediator;
        //holder handlers til alle events
        private readonly Dictionary<string, List<Type>> _handlers;
        //list af event typer
        private readonly List<Type> _eventTypes;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RabbitMQBus(IMediator mediator, IServiceScopeFactory serviceScopeFactory)
        {
            _mediator = mediator;
            _serviceScopeFactory = serviceScopeFactory;
            _handlers = new Dictionary<string, List<Type>>();
            _eventTypes = new List<Type>();
        }
        public Task SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }

        public void PublishEvent<T>(T @event) where T : Event
        {
            var factory = new ConnectionFactory() { Uri = new Uri("amqp://admin:iamadmin@localhost:5672") };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                //finder type navnene af generic @event
                var eventName = @event.GetType().Name;

                //laver en kø med det nævnte navn eventName
                channel.QueueDeclare(eventName, true, false, false, null);
                var properties = channel.CreateBasicProperties();
                properties.Persistent = false;

                //konverter @event til JSON string
                var message = JsonConvert.SerializeObject(@event);
                //konverterer besked til bytes
                var body = Encoding.UTF8.GetBytes(message);

                //publishes body til den erklærede kø, med navn eventName
                channel.BasicPublish("", eventName, null, body);
            }
        }

        public void Subscribe<T, TH>()
            where T : Event
            where TH : IEventHandler<T>
        {
            //en måde at få event navn
            var eventName = typeof(T).Name;
            //finder eventHandler type
            var handlerType = typeof(TH);

            //hvis listen ikke allerede indholder event typen, tilføjer event typen
            if (!_eventTypes.Contains(typeof(T)))
            {
                _eventTypes.Add(typeof(T));
            }

            //tjekker om dictionary keys allerede eksisterer det event navn
            if (!_handlers.ContainsKey(eventName))
            {
                _handlers.Add(eventName, new List<Type>());
            }

            //basic validation
            //hvis en handler i det eventName(key) allerede eksisterer, throw exception
            if (_handlers[eventName].Any(s => s.GetType() == handlerType))
            {
                throw new ArgumentException($"Handler, of type {handlerType.Name} is already registered for '{eventName}'", nameof(handlerType));
            }

            //tilføjer handlertype til list, med den allerede defineret string key, med eventName værdien
            _handlers[eventName].Add(handlerType);

            StartBasicConsume<T>();
        }

        private void StartBasicConsume<T>() where T : Event
        {
            var factory = new ConnectionFactory() { Uri = new Uri("amqp://admin:iamadmin@localhost:5672"), DispatchConsumersAsync = true };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            //får fat i navnet på type T
            var eventName = typeof(T).Name;

            channel.QueueDeclare(eventName, true, false, false, null);

            //async consumer
            var consumer = new AsyncEventingBasicConsumer(channel);
            //delegate for den modtagede besked (+= definerer en delegate)
            //consumer.Received += Consumer_Received;

            channel.BasicConsume(eventName, true, consumer);
        }

        //private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        //{
        //    var eventName = @event.RoutingKey;
        //    var message = Encoding.UTF8.GetString(@event.Body);
            
        //    try
        //    {
        //        //ProccesEvent ved hvilken hander der er subscribed til denne type af event
        //        await ProcessEvent(eventName, message).ConfigureAwait(false);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        throw;
        //    }
        //}

        private async Task ProcessEvent(string eventName, string message)
        {
            //tjekker dictionary for at se om der allerede er den specificeret type af hander, baseret på key
            if (_handlers.ContainsKey(eventName))
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var subscriptions = _handlers[eventName];
                    foreach (var subscription in subscriptions)
                    {
                        //laver ny subscription event
                        var handler = scope.ServiceProvider.GetService(subscription);
                        if (handler == null) continue;
                        var eventType = _eventTypes.SingleOrDefault(t => t.Name == eventName);
                        var @event = JsonConvert.DeserializeObject(message, eventType);
                        var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType);

                        await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { @event });
                    }
                }
            }
        }
    }
}