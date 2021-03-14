using System;
using RabbitMQ.Bus.Events;

namespace RabbitMQ.Bus.Commands
{
    public abstract class Command : Message
    {
        public DateTime TimeStamp { get; protected set; }

        protected Command()
        {
            TimeStamp = DateTime.Now;
        }
    }
}
