using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailerStores.Application.Dto;
using RetailerStores.Application.Interfaces;
using RetailerStores.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailerStores.Application.Stores.Queries
{
    public class GetStoreQuery : IRequest<StoreExtendedDto>
    {
        [Required]
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetStoreQuery, StoreExtendedDto>
        {
            private readonly IRetailerStoresDbContext _dbContext;
            private readonly IMapper _mapper;
            private readonly IValidator<GetStoreQuery> _validator;

            public Handler(IRetailerStoresDbContext dbContext, IMapper mapper, IValidator<GetStoreQuery> validator)
            {
                _dbContext = dbContext;
                _mapper = mapper;
                _validator = validator;
            }

            public async Task<StoreExtendedDto> Handle(GetStoreQuery request, CancellationToken cancellationToken)
            {
                if (_validator is not null)
                    await _validator.ValidateAndThrowAsync(request, cancellationToken);

                var entitiy = await _dbContext.Set<Store>().Include(a => a.Manager).AsNoTracking()
                    .FirstAsync(a => a.Id == request.Id);

                return _mapper.Map<StoreExtendedDto>(entitiy);
            }
        }

        public class Validator : AbstractValidator<GetStoreQuery>
        {
            public Validator(IRetailerStoresDbContext dbContext)
            {
                RuleFor(a => a.Id)
                    .MustAsync(async (id, ctx) => await dbContext.Set<Store>().AsNoTracking().AnyAsync(a => a.Id == id, ctx))
                    .WithMessage("Store with current identifier doesn't exist");
            }
        }
    }
}
