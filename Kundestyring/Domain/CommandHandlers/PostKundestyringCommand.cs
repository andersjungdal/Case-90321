using System;
using System.Collections.Generic;

namespace Kundestyring.Domain.CommandHandlers
{
    public abstract class PostKundestyringCommand : RabbitMQ.Bus.Commands.Command
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Categories { get; set; }
    }
}
