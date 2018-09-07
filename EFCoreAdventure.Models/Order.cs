using System;
using System.Collections.Generic;

namespace EFCoreAdventure.Models
{
    public class Order : Entity
    {
        public DateTime OrderDate { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public bool IsDraft { get; set; }


    }
}
