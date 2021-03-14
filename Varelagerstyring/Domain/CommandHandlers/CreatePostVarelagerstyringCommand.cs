using System;
using System.Collections.Generic;

namespace Varelagerstyring.Domain.CommandHandlers
{
    public class CreatePostVarelagerstyringCommand : PostVarelagerstyringCommand
    {
        public CreatePostVarelagerstyringCommand(int id, string name, string categories)
        {
            Id = id;
            Name = name;
            Categories = categories;
        }
    }
}
