namespace RetailerStores.Frontend.Models.Dto
{
    public class StoreExtendedDto : StoreDto
    {
        public ManagerDto Manager { get; set; } = new ManagerDto();
    }

    public static class Extensions
    {
        public static UpdateStoreModel ToUpdateStoreModel(this StoreExtendedDto dto)
        {
            return new UpdateStoreModel
            {
                Id = dto.Id,
                ManagerId = dto.Manager.Id,
                Name = dto.Name,
                CountryCode = dto.CountryCode,
                ManagerCategory = dto.Manager.Category,
                ManagerEmail = dto.Manager.Email,
                ManagerFirstName = dto.Manager.FirstName,
                ManagerLastName = dto.Manager.LastName,
                StoreEmail = dto.StoreEmail
            };
        }
    }
}
