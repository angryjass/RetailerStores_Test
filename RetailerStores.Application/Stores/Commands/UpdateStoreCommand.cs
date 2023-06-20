using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailerStores.Application.Dto;
using RetailerStores.Application.Interfaces;
using RetailerStores.Domain;
using System.ComponentModel.DataAnnotations;

namespace RetailerStores.Application.Stores.Commands
{
    public class UpdateStoreCommand : IRequest<StoreDto>
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid ManagerId { get; set; }
        public string? Name { get; set; }
        public string? CountryCode { get; set; }
        public string? StoreEmail { get; set; }
        public string? ManagerFirstName { get; set; }
        public string? ManagerLastName { get; set; }
        public string? ManagerEmail { get; set; }
        public int? ManagerCategory { get; set; }

        public class Handler : IRequestHandler<UpdateStoreCommand, StoreDto>
        {
            private readonly IRetailerStoresDbContext _dbContext;
            private readonly IMapper _mapper;
            private readonly IValidator<UpdateStoreCommand> _validator;

            public Handler(IRetailerStoresDbContext dbContext, IMapper mapper, IValidator<UpdateStoreCommand> validator)
            {
                _dbContext = dbContext;
                _mapper = mapper;
                _validator = validator;
            }

            public async Task<StoreDto> Handle(UpdateStoreCommand request, CancellationToken cancellationToken)
            {
                if (_validator is not null)
                    await _validator.ValidateAndThrowAsync(request, cancellationToken);

                var store = _mapper.Map<Store>(request);
                store.Manager = _mapper.Map<Manager>(request);

                var entity = _dbContext.Set<Store>().Update(store).Entity;
                await _dbContext.SaveChangesAsync(cancellationToken);

                return _mapper.Map<StoreDto>(entity);
            }
        }

        public class Validator : AbstractValidator<UpdateStoreCommand>
        {
            public Validator(IRetailerStoresDbContext dbContext)
            {
                RuleFor(a => a.Name)
                    .Must((str) => !string.IsNullOrWhiteSpace(str))
                    .WithMessage("Name must be not empty");

                RuleFor(a => a.CountryCode)
                    .Must((str) => !string.IsNullOrWhiteSpace(str))
                    .WithMessage("CountryCode must be not empty");

                RuleFor(a => a.StoreEmail)
                    .Must((str) => !string.IsNullOrWhiteSpace(str))
                    .WithMessage("StoreEmail must be not empty");

                RuleFor(a => a.ManagerFirstName)
                    .Must((str) => !string.IsNullOrWhiteSpace(str))
                    .WithMessage("ManagerFirstName must be not empty");

                RuleFor(a => a.ManagerLastName)
                    .Must((str) => !string.IsNullOrWhiteSpace(str))
                    .WithMessage("ManagerLastName must be not empty");

                RuleFor(a => a.ManagerEmail)
                    .Must((str) => !string.IsNullOrWhiteSpace(str))
                    .WithMessage("ManagerEmail must be not empty");

                RuleFor(a => a.ManagerCategory)
                    .Must((num) => num >= 0)
                    .WithMessage("ManagerCategory must be more then or equal zero");

                RuleFor(a => a.ManagerId)
                    .MustAsync(async (id, ctx) => await dbContext.Set<Manager>().AsNoTracking().AnyAsync(a => a.Id == id, ctx))
                    .WithMessage("Manager with current identifier doesn't exist");

                RuleFor(a => a.Id)
                    .MustAsync(async (id, ctx) => await dbContext.Set<Store>().AsNoTracking().AnyAsync(a => a.Id == id, ctx))
                    .WithMessage("Store with current identifier doesn't exist");
            }
        }
    }
}
