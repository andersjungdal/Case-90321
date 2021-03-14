using System;
using System.Collections.Generic;
using RabbitMQ.Bus.Events;

namespace Billedstyring.Domain.Events
{
    public class PostOrderCreatedEvent : Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Categories { get; set; }

        public PostOrderCreatedEvent(int id, string name, string categories)
        {
            Id = id;
            Name = name;
            Categories = categories;
        }
    }
}
