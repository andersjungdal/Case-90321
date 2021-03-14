using System;
using System.Collections.Generic;

namespace Billedstyring.Domain.CommandHandlers
{
    public class CreatePostBilledstyringCommand : PostBilledstyringCommand
    {
        public CreatePostBilledstyringCommand(int id, string name, string categories)
        {
            Id = id;
            Name = name;
            Categories = categories;
        }
    }
}
