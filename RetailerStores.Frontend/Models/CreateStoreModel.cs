using System.ComponentModel.DataAnnotations;

namespace RetailerStores.Frontend.Models
{
    public class CreateStoreModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string CountryCode { get; set; } = string.Empty;
        [Required]
        public string StoreEmail { get; set; } = string.Empty;
        [Required]
        public string ManagerFirstName { get; set; } = string.Empty;
        [Required]
        public string ManagerLastName { get; set; } = string.Empty;
        [Required]
        public string ManagerEmail { get; set; } = string.Empty;
        [Required]
        public int ManagerCategory { get; set; }
    }
}
