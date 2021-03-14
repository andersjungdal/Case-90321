using System;
using System.Collections.Generic;

namespace Billedstyring.Domain.CommandHandlers
{
    public abstract class PostBilledstyringCommand : RabbitMQ.Bus.Commands.Command
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Categories { get; set; }
    }
}
