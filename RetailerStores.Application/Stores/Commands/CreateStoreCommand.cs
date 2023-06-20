using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Internal;
using RetailerStores.Application.Dto;
using RetailerStores.Application.Interfaces;
using RetailerStores.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailerStores.Application.Stores.Commands
{
    public class CreateStoreCommand : IRequest<StoreDto>
    {
        public string Name { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        public string StoreEmail { get; set; } = string.Empty;
        public string ManagerFirstName { get; set; } = string.Empty;
        public string ManagerLastName { get; set; } = string.Empty;
        public string ManagerEmail { get; set; } = string.Empty;
        public int ManagerCategory { get; set; }

        public class Handler : IRequestHandler<CreateStoreCommand, StoreDto>
        {
            private readonly IRetailerStoresDbContext _dbContext;
            private readonly IMapper _mapper;
            private readonly IValidator<CreateStoreCommand> _validator;

            public Handler(IRetailerStoresDbContext dbContext, IMapper mapper, IValidator<CreateStoreCommand> validator)
            {
                _dbContext = dbContext;
                _mapper = mapper;
                _validator = validator;
            }

            public async Task<StoreDto> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
            {
                if (_validator is not null)
                    await _validator.ValidateAndThrowAsync(request, cancellationToken);

                var newStore = _mapper.Map<Store>(request);
                newStore.Manager = _mapper.Map<Manager>(request);

                var entity = await _dbContext.Set<Store>().AddAsync(newStore, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return _mapper.Map<StoreDto>(entity.Entity);
            }
        }

        public class Validator : AbstractValidator<CreateStoreCommand>
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
            }
        }
    }
}
