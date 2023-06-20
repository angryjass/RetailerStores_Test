using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailerStores.Frontend.Models.Dto
{
    public class StockDto : BaseDto
    {
        public int Backstock { get; set; }
        public int Frontstock { get; set; }
        public int ShoppingWindow { get; set; }
        public decimal Accuracy { get; set; }
        public decimal OnFloorAvailability { get; set; }
        public int MeanAge { get; set; }
        public DateTimeOffset RecordingDate { get; set; }
        public Guid StoreId { get; set; }
    }
}
