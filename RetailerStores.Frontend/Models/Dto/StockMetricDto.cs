using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailerStores.Frontend.Models.Dto
{
    public class StockMetricDto
    {
        public decimal[] Values { get; set; } = Array.Empty<decimal>();
    }
}
