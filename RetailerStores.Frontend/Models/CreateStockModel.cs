using System.ComponentModel.DataAnnotations;

namespace RetailerStores.Frontend.Models
{
    public class CreateStockModel
    {
        public int Backstock { get; set; }
        public int Frontstock { get; set; }
        public int ShoppingWindow { get; set; }
        public decimal Accuracy { get; set; }
        public decimal OnFloorAvailability { get; set; }
        public int MeanAge { get; set; }
        [Required]
        public Guid StoreId { get; set; }
    }
}
