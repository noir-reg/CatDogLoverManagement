using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLoverManagement.Repository.Models.ViewModels
{
    public class ViewOrderService
    {
        public Guid OrderId { get; set; }
        public string BuyerName { get; set; }
        public string SellerName { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? BookedDate { get; set; }
        public TimeSpan? From { get; set; }
        public TimeSpan? To { get; set; }
        public string Status { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public string Title { get; set; } = null!;
        public ServiceModel? ServiceModel { get; set; }
        public ViewSellOrGivePost? PostModel { get; set; }
    }
}
