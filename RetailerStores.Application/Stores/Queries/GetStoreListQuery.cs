using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailerStores.Application.Dto;
using RetailerStores.Application.Interfaces;
using RetailerStores.Domain;

namespace RetailerStores.Application.Stores.Queries
{
    public class GetStoreListQuery : IRequest<List<StoreDto>>
    {
        public class Handler : IRequestHandler<GetStoreListQuery, List<StoreDto>>
        {
            private readonly IRetailerStoresDbContext _dbContext;
            private readonly IMapper _mapper;

            public Handler(IRetailerStoresDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }

            public async Task<List<StoreDto>> Handle(GetStoreListQuery request, CancellationToken cancellationToken)
            {
                var entities = await _dbContext.Set<Store>().AsNoTracking()
                    .ToListAsync(cancellationToken);

                return _mapper.Map<List<StoreDto>>(entities);
            }
        }
    }
}
