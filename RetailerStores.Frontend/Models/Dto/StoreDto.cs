using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailerStores.Frontend.Models.Dto
{
    public class StoreDto : BaseDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string CountryCode { get; set; } = string.Empty;
        [Required]
        public string StoreEmail { get; set; } = string.Empty;
    }
}
