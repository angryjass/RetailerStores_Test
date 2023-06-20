using Microsoft.EntityFrameworkCore;
using RetailerStores.Application.Stores.Commands;
using RetailerStores.Domain;

namespace RetailerStores.WebApi.Tests.Stores.Commands
{
    public class UpdateStoreCommandTests : InMemoryTestBase
    {
        private UpdateStoreCommand.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _handler = new UpdateStoreCommand.Handler(DbContext, Mapper, new UpdateStoreCommand.Validator(DbContext));
        }

        [Test]
        public void IsWorking_Test()
        {
            var store = DbContext.Set<Store>().AsNoTracking().First();
            var request = new UpdateStoreCommand()
            {
                Id = store.Id,
                Name = "test",
                CountryCode = "test",
                ManagerCategory = 5,
                ManagerEmail = "test@test.test",
                ManagerFirstName = "test",
                ManagerLastName = "test",
                StoreEmail = "test@tets.tes",
                ManagerId = store.ManagerId
            };

            Assert.DoesNotThrowAsync(async () => await _handler.Handle(request, CancellationToken));
        }

        [Test]
        public async Task FieldsIsTruelySave_Test()
        {
            var store = DbContext.Set<Store>().Include(a => a.Manager).AsNoTracking().First();

            var request = new UpdateStoreCommand()
            {
                Id = store.Id,
                Name = "123",
                CountryCode = "123",
                ManagerCategory = 769,
                ManagerEmail = "123123",
                ManagerFirstName = "123",
                ManagerLastName = "123",
                StoreEmail = "123123",
                ManagerId = store.ManagerId
            };

            var newStoreDto = await _handler.Handle(request, CancellationToken);

            var storeFromDb = DbContext.Set<Store>().Include(a => a.Manager).AsNoTracking().First(a => a.Id == newStoreDto.Id);

            Assert.Multiple(() =>
            {
                Assert.That(storeFromDb.Id, Is.EqualTo(request.Id));
                Assert.That(storeFromDb.Name, Is.EqualTo(request.Name));
                Assert.That(storeFromDb.CountryCode, Is.EqualTo(request.CountryCode));
                Assert.That(storeFromDb.Manager.Category, Is.EqualTo(request.ManagerCategory));
                Assert.That(storeFromDb.Manager.Email, Is.EqualTo(request.ManagerEmail));
                Assert.That(storeFromDb.Manager.FirstName, Is.EqualTo(request.ManagerFirstName));
                Assert.That(storeFromDb.Manager.LastName, Is.EqualTo(request.ManagerLastName));
                Assert.That(storeFromDb.Manager.Id, Is.EqualTo(request.ManagerId));
                Assert.That(storeFromDb.StoreEmail, Is.EqualTo(request.StoreEmail));
            });
        }

        [Test]
        public void IsValidationWorks_Test()
        {
            var store = DbContext.Set<Store>().AsNoTracking().First();

            var request = new UpdateStoreCommand()
            {
                Id = store.Id,
                Name = "",
                CountryCode = "test",
                ManagerCategory = 5,
                ManagerEmail = "test@test.test",
                ManagerFirstName = "test",
                ManagerLastName = "test",
                StoreEmail = "test@tets.tes",
                ManagerId = store.ManagerId
            };

            Assert.ThrowsAsync<FluentValidation.ValidationException>(async () => await _handler.Handle(request, CancellationToken));

            request = new UpdateStoreCommand()
            {
                Id = store.Id,
                Name = "test",
                CountryCode = "",
                ManagerCategory = 5,
                ManagerEmail = "test@test.test",
                ManagerFirstName = "test",
                ManagerLastName = "test",
                StoreEmail = "test@tets.tes",
                ManagerId = store.ManagerId
            };

            Assert.ThrowsAsync<FluentValidation.ValidationException>(async () => await _handler.Handle(request, CancellationToken));

            request = new UpdateStoreCommand()
            {
                Id = store.Id,
                Name = "test",
                CountryCode = "test",
                ManagerCategory = -1,
                ManagerEmail = "test@test.test",
                ManagerFirstName = "test",
                ManagerLastName = "test",
                StoreEmail = "test@tets.tes",
                ManagerId = store.ManagerId
            };

            Assert.ThrowsAsync<FluentValidation.ValidationException>(async () => await _handler.Handle(request, CancellationToken));

            request = new UpdateStoreCommand()
            {
                Id = store.Id,
                Name = "test",
                CountryCode = "test",
                ManagerCategory = 5,
                ManagerEmail = "",
                ManagerFirstName = "test",
                ManagerLastName = "test",
                StoreEmail = "test@tets.tes",
                ManagerId = store.ManagerId
            };

            Assert.ThrowsAsync<FluentValidation.ValidationException>(async () => await _handler.Handle(request, CancellationToken));

            request = new UpdateStoreCommand()
            {
                Id = store.Id,
                Name = "test",
                CountryCode = "test",
                ManagerCategory = 5,
                ManagerEmail = "test@test.test",
                ManagerFirstName = "",
                ManagerLastName = "test",
                StoreEmail = "test@tets.tes",
                ManagerId = store.ManagerId
            };

            Assert.ThrowsAsync<FluentValidation.ValidationException>(async () => await _handler.Handle(request, CancellationToken));

            request = new UpdateStoreCommand()
            {
                Id = store.Id,
                Name = "test",
                CountryCode = "test",
                ManagerCategory = 5,
                ManagerEmail = "test@test.test",
                ManagerFirstName = "test",
                ManagerLastName = "",
                StoreEmail = "test@tets.tes",
                ManagerId = store.ManagerId
            };

            Assert.ThrowsAsync<FluentValidation.ValidationException>(async () => await _handler.Handle(request, CancellationToken));

            request = new UpdateStoreCommand()
            {
                Id = store.Id,
                Name = "test",
                CountryCode = "test",
                ManagerCategory = 5,
                ManagerEmail = "test@test.test",
                ManagerFirstName = "test",
                ManagerLastName = "test",
                StoreEmail = "",
                ManagerId = store.ManagerId
            };

            Assert.ThrowsAsync<FluentValidation.ValidationException>(async () => await _handler.Handle(request, CancellationToken));

            request = new UpdateStoreCommand()
            {
                Id = store.Id,
                Name = "test",
                CountryCode = "test",
                ManagerCategory = 5,
                ManagerEmail = "test@test.test",
                ManagerFirstName = "test",
                ManagerLastName = "test",
                StoreEmail = "test@test.te",
                ManagerId = Guid.NewGuid()
            };

            Assert.ThrowsAsync<FluentValidation.ValidationException>(async () => await _handler.Handle(request, CancellationToken));
        }
    }
}