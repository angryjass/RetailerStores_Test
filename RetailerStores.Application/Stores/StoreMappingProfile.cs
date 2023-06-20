using AutoMapper;
using RetailerStores.Application.Dto;
using RetailerStores.Application.Stores.Commands;
using RetailerStores.Domain;

namespace RetailerStores.Application.Stores
{
    public class StoreMappingProfile : Profile
    {
        public StoreMappingProfile()
        {
            CreateMap<CreateStoreCommand, Store>();
            CreateMap<CreateStoreCommand, Manager>()
                .ForMember(a => a.Email,
                    o => o.MapFrom(c => c.ManagerEmail))
                .ForMember(a => a.FirstName,
                    o => o.MapFrom(c => c.ManagerFirstName))
                .ForMember(a => a.LastName,
                    o => o.MapFrom(c => c.ManagerLastName))
                .ForMember(a => a.Category,
                    o => o.MapFrom(c => c.ManagerCategory));

            CreateMap<UpdateStoreCommand, Store>();
            CreateMap<UpdateStoreCommand, Manager>()
                .ForMember(a => a.Id,
                    o => o.MapFrom(c => c.ManagerId))
                .ForMember(a => a.Email,
                    o => o.MapFrom(c => c.ManagerEmail))
                .ForMember(a => a.FirstName,
                    o => o.MapFrom(c => c.ManagerFirstName))
                .ForMember(a => a.LastName,
                    o => o.MapFrom(c => c.ManagerLastName))
                .ForMember(a => a.Category,
                    o => o.MapFrom(c => c.ManagerCategory));

            CreateMap<Store, StoreDto>();
            CreateMap<Manager, ManagerDto>();
            CreateMap<Store, StoreExtendedDto>();
        }
    }
}
