namespace RetailerStores.Frontend.Models
{
    public class UpdateStoreModel
    {
        public Guid Id { get; set; }
        public Guid ManagerId { get; set; }
        public string? Name { get; set; }
        public string? CountryCode { get; set; }
        public string? StoreEmail { get; set; }
        public string? ManagerFirstName { get; set; }
        public string? ManagerLastName { get; set; }
        public string? ManagerEmail { get; set; }
        public int? ManagerCategory { get; set; }
    }
}
