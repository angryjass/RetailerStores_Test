using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailerStores.Application.Dto
{
    public class StoreDto : BaseDto
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Country code
        /// </summary>
        public string CountryCode { get; set; } = string.Empty;

        /// <summary>
        /// Email address
        /// </summary>
        public string StoreEmail { get; set; } = string.Empty;
    }
}
