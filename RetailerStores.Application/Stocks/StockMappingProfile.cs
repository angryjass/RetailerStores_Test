using AutoMapper;
using RetailerStores.Application.Dto;
using RetailerStores.Application.Stocks.Commands;
using RetailerStores.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailerStores.Application.Stocks
{
    public class StockMappingProfile : Profile
    {
        public StockMappingProfile() 
        {
            CreateMap<CreateStockCommand, Stock>();
            CreateMap<Stock, StockDto>();
        }
    }
}
