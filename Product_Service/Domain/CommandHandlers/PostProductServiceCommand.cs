using System;
using System.Collections.Generic;

namespace Product_Service.Domain.CommandHandlers
{
    public abstract class PostProductServiceCommand : RabbitMQ.Bus.Commands.Command
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Categories { get; set; }
    }
}
