using System;
using System.Collections.Generic;

namespace CatDogLoverManagement.Repository.Models
{
    public partial class Transaction
    {
        public Guid TransactionId { get; set; }
        public Guid OrderId { get; set; }

        public virtual Order Order { get; set; } = null!;
    }
}
