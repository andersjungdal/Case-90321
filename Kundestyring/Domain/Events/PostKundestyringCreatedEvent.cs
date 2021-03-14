using System;
using System.Collections.Generic;
using RabbitMQ.Bus.Events;

namespace Kundestyring.Domain.Events
{
    public class PostKundestyringCreatedEvent : Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Categories { get; set; }

        public PostKundestyringCreatedEvent(int id, string name, string categories)
        {
            Id = id;
            Name = name;
            Categories = categories;
        }
    }
}
