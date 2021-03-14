using System;
using System.Collections.Generic;

namespace Product_Service.Domain.CommandHandlers
{
    public class CreatePostProductServiceCommand : PostProductServiceCommand
    {
        public CreatePostProductServiceCommand(int id, string name, string categories)
        {
            Id = id;
            Name = name;
            Categories = categories;
        }
    }
}
