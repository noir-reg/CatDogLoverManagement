using System;
using System.Collections.Generic;

namespace CatDogLoverManagement.Repository.Models
{
    public partial class Order
    {
        public Order()
        {
            Transactions = new HashSet<Transaction>();
        }

        public Guid OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? BookedDate { get; set; }
        public string? BookedTime { get; set; }
        public string? Status { get; set; } = null!;
        public decimal? Price { get; set; }
        public Guid? BuyerId { get; set; }
        public Guid? SellerId { get; set; }
        public Guid? AnimalId { get; set; }
        public Guid? ServiceId { get; set; }

        public virtual Animal? Animal { get; set; }
        public virtual User Buyer { get; set; } = null!;
        public virtual User Seller { get; set; } = null!;
        public virtual Service? Service { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
