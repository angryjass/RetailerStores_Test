using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailerStores.Application.Dto
{
    public class StoreExtendedDto : StoreDto
    {
        public ManagerDto Manager { get; set; } = new ManagerDto();
    }
}
