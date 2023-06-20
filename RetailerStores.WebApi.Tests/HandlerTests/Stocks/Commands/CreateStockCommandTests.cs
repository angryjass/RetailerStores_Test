using Microsoft.EntityFrameworkCore;
using RetailerStores.Application.Stocks.Commands;
using RetailerStores.Domain;

namespace RetailerStores.WebApi.Tests.Stocks.Commands
{
    public class CreateStockCommandTests : InMemoryTestBase
    {
        private CreateStockCommand.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _handler = new CreateStockCommand.Handler(DbContext, Mapper, Clock, new CreateStockCommand.Validator(DbContext));
        }

        [Test]
        public void IsWorking_Test()
        {
            var request = new CreateStockCommand()
            {
                Accuracy = 1,
                Backstock = 2,
                Frontstock = 3,
                MeanAge = 4,
                OnFloorAvailability = 5,
                ShoppingWindow = 6,
                StoreId = DbContext.Set<Store>().AsNoTracking().First().Id,
            };

            Assert.DoesNotThrowAsync(async () => await _handler.Handle(request, CancellationToken));
        }

        [Test]
        public async Task IsAddStockToStore_Test()
        {
            var request = new CreateStockCommand()
            {
                Accuracy = 1,
                Backstock = 2,
                Frontstock = 3,
                MeanAge = 4,
                OnFloorAvailability = 5,
                ShoppingWindow = 6,
                StoreId = DbContext.Set<Store>().AsNoTracking().First().Id,
            };

            var newStock = await _handler.Handle(request, CancellationToken);

            var currentStockInfo = DbContext.Set<Store>().AsNoTracking()
                .Include(a => a.Stocks)
                .First(a => a.Id == request.StoreId)
                .Stocks
                .OrderBy(a => a.RecordingDate).Last();

            Assert.That(currentStockInfo.Id, Is.EqualTo(newStock.Id));
        }

        [Test]
        public async Task FieldsIsTruelySave_Test()
        {
            var request = new CreateStockCommand()
            {
                Accuracy = 1,
                Backstock = 2,
                Frontstock = 3,
                MeanAge = 4,
                OnFloorAvailability = 5,
                ShoppingWindow = 6,
                StoreId = DbContext.Set<Store>().AsNoTracking().First().Id,
            };

            var newStock = await _handler.Handle(request, CancellationToken);

            var currentStockInfo = DbContext.Set<Store>().AsNoTracking()
                .Include(a => a.Stocks)
                .First(a => a.Id == request.StoreId)
                .Stocks
                .OrderBy(a => a.RecordingDate).Last();

            Assert.Multiple(() =>
            {
                Assert.That(currentStockInfo.Accuracy, Is.EqualTo(request.Accuracy));
                Assert.That(currentStockInfo.Backstock, Is.EqualTo(request.Backstock));
                Assert.That(currentStockInfo.Frontstock, Is.EqualTo(request.Frontstock));
                Assert.That(currentStockInfo.MeanAge, Is.EqualTo(request.MeanAge));
                Assert.That(currentStockInfo.OnFloorAvailability, Is.EqualTo(request.OnFloorAvailability));
                Assert.That(currentStockInfo.ShoppingWindow, Is.EqualTo(request.ShoppingWindow));
            });
        }

        [Test]
        public void IsValidationWorks_Test()
        {
            var request = new CreateStockCommand()
            {
                Accuracy = -1,
                Backstock = 2,
                Frontstock = 3,
                MeanAge = 4,
                OnFloorAvailability = 5,
                ShoppingWindow = 6,
                StoreId = DbContext.Set<Store>().AsNoTracking().First().Id,
            };

            Assert.ThrowsAsync<FluentValidation.ValidationException>(async () => await _handler.Handle(request, CancellationToken));

            request = new CreateStockCommand()
            {
                Accuracy = 1,
                Backstock = -2,
                Frontstock = 3,
                MeanAge = 4,
                OnFloorAvailability = 5,
                ShoppingWindow = 6,
                StoreId = DbContext.Set<Store>().AsNoTracking().First().Id,
            };

            Assert.ThrowsAsync<FluentValidation.ValidationException>(async () => await _handler.Handle(request, CancellationToken));

            request = new CreateStockCommand()
            {
                Accuracy = 1,
                Backstock = 2,
                Frontstock = -3,
                MeanAge = 4,
                OnFloorAvailability = 5,
                ShoppingWindow = 6,
                StoreId = DbContext.Set<Store>().AsNoTracking().First().Id,
            };

            Assert.ThrowsAsync<FluentValidation.ValidationException>(async () => await _handler.Handle(request, CancellationToken));

            request = new CreateStockCommand()
            {
                Accuracy = 1,
                Backstock = 2,
                Frontstock = 3,
                MeanAge = -4,
                OnFloorAvailability = 5,
                ShoppingWindow = 6,
                StoreId = DbContext.Set<Store>().AsNoTracking().First().Id,
            };

            Assert.ThrowsAsync<FluentValidation.ValidationException>(async () => await _handler.Handle(request, CancellationToken));

            request = new CreateStockCommand()
            {
                Accuracy = 1,
                Backstock = 2,
                Frontstock = 3,
                MeanAge = 4,
                OnFloorAvailability = -5,
                ShoppingWindow = 6,
                StoreId = DbContext.Set<Store>().AsNoTracking().First().Id,
            };

            Assert.ThrowsAsync<FluentValidation.ValidationException>(async () => await _handler.Handle(request, CancellationToken));

            request = new CreateStockCommand()
            {
                Accuracy = 1,
                Backstock = 2,
                Frontstock = 3,
                MeanAge = 4,
                OnFloorAvailability = 5,
                ShoppingWindow = -6,
                StoreId = DbContext.Set<Store>().AsNoTracking().First().Id,
            };

            Assert.ThrowsAsync<FluentValidation.ValidationException>(async () => await _handler.Handle(request, CancellationToken));

            request = new CreateStockCommand()
            {
                Accuracy = 1,
                Backstock = 2,
                Frontstock = 3,
                MeanAge = 4,
                OnFloorAvailability = 5,
                ShoppingWindow = 6,
                StoreId = Guid.NewGuid(),
            };

            Assert.ThrowsAsync<FluentValidation.ValidationException>(async () => await _handler.Handle(request, CancellationToken));
        }
    }
}