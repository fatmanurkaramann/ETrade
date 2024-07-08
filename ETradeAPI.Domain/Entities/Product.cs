using ETradeAPI.Domain.Entities.Comman;
using static System.Net.Mime.MediaTypeNames;

namespace ETradeAPI.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
        //public ICollection<Order> Orders { get; set; }
        public string? FileName { get; set; }
        public string? Path { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; }

    }
}
