using System;
using System.Collections.Generic;

namespace Varelagerstyring.Domain.CommandHandlers
{
    public abstract class PostVarelagerstyringCommand : RabbitMQ.Bus.Commands.Command
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Categories { get; set; }
    }
}
