using System;
using System.Collections.Generic;
using Flunt.Notifications;
using Store.Domain.Commands.Interfaces;

namespace Store.Domain.Commands
{
    public class CreateOrderCommand : Notifiable, ICommand
    {
        public string Customer { get; set; }
        public string ZipCode { get; set; }
        public string PromoCode { get; set; }
        public IList<CreateOrderItemCommand> Items { get; set; }

        public void Validate()
        {
            throw new NotImplementedException();
        }
    }
}