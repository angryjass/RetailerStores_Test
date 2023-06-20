using Microsoft.EntityFrameworkCore;
using RetailerStores.Application.Stores.Commands;
using RetailerStores.Domain;

namespace RetailerStores.WebApi.Tests.Stores.Commands
{
    public class CreateStoreCommandTests : InMemoryTestBase
    {
        private CreateStoreCommand.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _handler = new CreateStoreCommand.Handler(DbContext, Mapper, new CreateStoreCommand.Validator(DbContext));
        }

        [Test]
        public void IsWorking_Test()
        {
            var request = new CreateStoreCommand()
            {
                Name = "test",
                CountryCode = "test",
                ManagerCategory = 5,
                ManagerEmail = "test@test.test",
                ManagerFirstName = "test",
                ManagerLastName = "test",
                StoreEmail = "test@tets.tes"
            };

            Assert.DoesNotThrowAsync(async () => await _handler.Handle(request, CancellationToken));
        }

        [Test]
        public async Task IsAddManagerToStore_Test()
        {
            var request = new CreateStoreCommand()
            {
                Name = "test",
                CountryCode = "test",
                ManagerCategory = 5,
                ManagerEmail = "test@test.test",
                ManagerFirstName = "test",
                ManagerLastName = "test",
                StoreEmail = "test@tets.tes"
            };

            var newStoreDto = await _handler.Handle(request, CancellationToken);

            Assert.That(
                DbContext.Set<Manager>().AsNoTracking()
                    .Include(a => a.Store)
                    .Where(a => a.Store.Id == newStoreDto.Id)
                    .FirstOrDefault(), 
                Is.Not.Null);
        }

        [Test]
        public async Task FieldsIsTruelySave_Test()
        {
            var request = new CreateStoreCommand()
            {
                Name = "test",
                CountryCode = "test",
                ManagerCategory = 5,
                ManagerEmail = "test@test.test",
                ManagerFirstName = "test",
                ManagerLastName = "test",
                StoreEmail = "test@tets.tes"
            };

            var newStoreDto = await _handler.Handle(request, CancellationToken);

            var storeFromDb = DbContext.Set<Store>().Include(a => a.Manager).AsNoTracking().First(a => a.Id == newStoreDto.Id);

            Assert.Multiple(() =>
            {
                Assert.That(storeFromDb.Name, Is.EqualTo(request.Name));
                Assert.That(storeFromDb.CountryCode, Is.EqualTo(request.CountryCode));
                Assert.That(storeFromDb.Manager.Category, Is.EqualTo(request.ManagerCategory));
                Assert.That(storeFromDb.Manager.Email, Is.EqualTo(request.ManagerEmail));
                Assert.That(storeFromDb.Manager.FirstName, Is.EqualTo(request.ManagerFirstName));
                Assert.That(storeFromDb.Manager.LastName, Is.EqualTo(request.ManagerLastName));
                Assert.That(storeFromDb.StoreEmail, Is.EqualTo(request.StoreEmail));
            });
        }

        [Test]
        public void IsValidationWorks_Test()
        {
            var request = new CreateStoreCommand()
            {
                Name = "",
                CountryCode = "test",
                ManagerCategory = 5,
                ManagerEmail = "test@test.test",
                ManagerFirstName = "test",
                ManagerLastName = "test",
                StoreEmail = "test@tets.tes"
            };

            Assert.ThrowsAsync<FluentValidation.ValidationException>(async () => await _handler.Handle(request, CancellationToken));

            request = new CreateStoreCommand()
            {
                Name = "test",
                CountryCode = "",
                ManagerCategory = 5,
                ManagerEmail = "test@test.test",
                ManagerFirstName = "test",
                ManagerLastName = "test",
                StoreEmail = "test@tets.tes"
            };

            Assert.ThrowsAsync<FluentValidation.ValidationException>(async () => await _handler.Handle(request, CancellationToken));

            request = new CreateStoreCommand()
            {
                Name = "test",
                CountryCode = "test",
                ManagerCategory = -1,
                ManagerEmail = "test@test.test",
                ManagerFirstName = "test",
                ManagerLastName = "test",
                StoreEmail = "test@tets.tes"
            };

            Assert.ThrowsAsync<FluentValidation.ValidationException>(async () => await _handler.Handle(request, CancellationToken));

            request = new CreateStoreCommand()
            {
                Name = "test",
                CountryCode = "test",
                ManagerCategory = 5,
                ManagerEmail = "",
                ManagerFirstName = "test",
                ManagerLastName = "test",
                StoreEmail = "test@tets.tes"
            };

            Assert.ThrowsAsync<FluentValidation.ValidationException>(async () => await _handler.Handle(request, CancellationToken));

            request = new CreateStoreCommand()
            {
                Name = "test",
                CountryCode = "test",
                ManagerCategory = 5,
                ManagerEmail = "test@test.test",
                ManagerFirstName = "",
                ManagerLastName = "test",
                StoreEmail = "test@tets.tes"
            };

            Assert.ThrowsAsync<FluentValidation.ValidationException>(async () => await _handler.Handle(request, CancellationToken));

            request = new CreateStoreCommand()
            {
                Name = "test",
                CountryCode = "test",
                ManagerCategory = 5,
                ManagerEmail = "test@test.test",
                ManagerFirstName = "test",
                ManagerLastName = "",
                StoreEmail = "test@tets.tes"
            };

            Assert.ThrowsAsync<FluentValidation.ValidationException>(async () => await _handler.Handle(request, CancellationToken));

            request = new CreateStoreCommand()
            {
                Name = "test",
                CountryCode = "test",
                ManagerCategory = 5,
                ManagerEmail = "test@test.test",
                ManagerFirstName = "test",
                ManagerLastName = "test",
                StoreEmail = ""
            };

            Assert.ThrowsAsync<FluentValidation.ValidationException>(async () => await _handler.Handle(request, CancellationToken));
        }
    }
}