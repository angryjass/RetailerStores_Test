using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Internal;
using RetailerStores.Application.Dto;
using RetailerStores.Application.Interfaces;
using RetailerStores.Domain;

namespace RetailerStores.Application.Stocks.Commands
{
    public class CreateStockCommand : IRequest<StockDto>
    {
        public int Backstock { get; set; }
        public int Frontstock { get; set; }
        public int ShoppingWindow { get; set; }
        public decimal Accuracy { get; set; }
        public decimal OnFloorAvailability { get; set; }
        public int MeanAge { get; set; }
        public Guid StoreId { get; set; }

        public class Handler : IRequestHandler<CreateStockCommand, StockDto>
        {
            private readonly IRetailerStoresDbContext _dbContext;
            private readonly IMapper _mapper;
            private readonly ISystemClock _clock;
            private readonly IValidator<CreateStockCommand> _validator;

            public Handler(IRetailerStoresDbContext dbContext, IMapper mapper, ISystemClock clock, IValidator<CreateStockCommand> validator)
            {
                _dbContext = dbContext;
                _mapper = mapper;
                _clock = clock;
                _validator = validator;
            }

            public async Task<StockDto> Handle(CreateStockCommand request, CancellationToken cancellationToken)
            {
                if (_validator is not null)
                    await _validator.ValidateAndThrowAsync(request, cancellationToken);

                var stock = _mapper.Map<Stock>(request);
                stock.RecordingDate = _clock.UtcNow;

                var entity = await _dbContext.Set<Stock>().AddAsync(stock, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return _mapper.Map<StockDto>(entity.Entity);
            }
        }

        public class Validator : AbstractValidator<CreateStockCommand>
        {
            public Validator(IRetailerStoresDbContext dbContext)
            {
                RuleFor(a => a.Backstock)
                    .Must((num) => num >= 0)
                    .WithMessage("Backstock must be more then or equal zero");

                RuleFor(a => a.Frontstock)
                    .Must((num) => num >= 0)
                    .WithMessage("Frontstock must be more then or equal zero");

                RuleFor(a => a.ShoppingWindow)
                    .Must((num) => num >= 0)
                    .WithMessage("ShoppingWindow must be more then or equal zero");

                RuleFor(a => a.Accuracy)
                    .Must((num) => num >= 0)
                    .WithMessage("Accuracy must be more then or equal zero");

                RuleFor(a => a.OnFloorAvailability)
                    .Must((num) => num >= 0)
                    .WithMessage("OnFloorAvailability must be more then or equal zero");

                RuleFor(a => a.MeanAge)
                    .Must((num) => num >= 0)
                    .WithMessage("MeanAge must be more then or equal zero");

                RuleFor(a => a.StoreId)
                    .MustAsync(async (id, ctx) => await dbContext.Set<Store>().AsNoTracking().AnyAsync(a => a.Id == id, ctx))
                    .WithMessage("Store with current identifier doesn't exist");
            }
        }
    }
}
