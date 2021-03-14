using System;
using System.Collections.Generic;

namespace Kundestyring.Domain.CommandHandlers
{
    public class CreatePostKundestyringCommand : PostKundestyringCommand
    {
        public CreatePostKundestyringCommand(int id, string name, string categories)
        {
            Id = id;
            Name = name;
            Categories = categories;
        }
    }
}
