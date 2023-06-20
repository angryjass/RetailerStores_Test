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

namespace RetailerStores.Application.Stocks.Queries
{
    public class GetStockQuery : IRequest<StockDto?>
    {
        [Required]
        public Guid StoreId { get; set; }

        public class Handler : IRequestHandler<GetStockQuery, StockDto?>
        {
            private readonly IRetailerStoresDbContext _dbContext;
            private readonly IMapper _mapper;
            private readonly IValidator<GetStockQuery> _validator;

            public Handler(IRetailerStoresDbContext dbContext, IMapper mapper, IValidator<GetStockQuery> validator)
            {
                _dbContext = dbContext;
                _mapper = mapper;
                _validator = validator;
            }

            public async Task<StockDto?> Handle(GetStockQuery request, CancellationToken cancellationToken)
            {
                if (_validator is not null)
                    await _validator.ValidateAndThrowAsync(request, cancellationToken);

                var entitiy = await _dbContext.Set<Stock>().AsNoTracking()
                    .OrderBy(a => a.RecordingDate)
                    .LastOrDefaultAsync(a => a.StoreId == request.StoreId);

                return _mapper.Map<StockDto?>(entitiy);
            }
        }

        public class Validator : AbstractValidator<GetStockQuery>
        {
            public Validator(IRetailerStoresDbContext dbContext)
            {
                RuleFor(a => a.StoreId)
                    .MustAsync(async (id, ctx) => await dbContext.Set<Store>().AsNoTracking().AnyAsync(a => a.Id == id, ctx))
                    .WithMessage("Store with current identifier doesn't exist");
            }
        }
    }
}
