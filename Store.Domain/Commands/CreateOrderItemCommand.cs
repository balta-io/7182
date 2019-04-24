using System;
using System.Collections.Generic;
using Store.Domain.Commands.Interfaces;
using Flunt.Notifications;

namespace Store.Domain.Commands
{

    public class CreateOrderItemCommand : Notifiable, ICommand
    {
        public CreateOrderItemCommand() { }

        public CreateOrderItemCommand(Guid product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public Guid Product { get; set; }
        public int Quantity { get; set; }

        public void Validate()
        {
            throw new NotImplementedException();
        }
    }
}