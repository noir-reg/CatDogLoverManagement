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

        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime BookedDate { get; set; }
        public DateTime BookedTime { get; set; }
        public string Status { get; set; } = null!;
        public decimal Price { get; set; }
        public string TypeService { get; set; } = null!;
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
        public int PostId { get; set; }

        public virtual User Buyer { get; set; } = null!;
        public virtual Post Post { get; set; } = null!;
        public virtual User Seller { get; set; } = null!;
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
