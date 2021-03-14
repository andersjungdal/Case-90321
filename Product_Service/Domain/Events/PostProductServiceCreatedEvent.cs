using System;
using System.Collections.Generic;
using RabbitMQ.Bus.Events;

namespace Product_Service.Domain.Events
{
    public class PostProductServiceCreatedEvent : Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Categories { get; set; }

        public PostProductServiceCreatedEvent(int id, string name, string categories)
        {
            Id = Id;
            Name = name;
            Categories = categories;
        }
    }
}
