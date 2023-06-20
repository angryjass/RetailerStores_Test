namespace RetailerStores.Domain
{
    public class Store : BaseEntity
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Country code
        /// </summary>
        public string CountryCode { get; set; } = string.Empty;

        /// <summary>
        /// Email address
        /// </summary>
        public string StoreEmail { get; set; } = string.Empty;

        /// <summary>
        /// Manager identifier
        /// </summary>
        public Guid ManagerId { get; set; }

        public virtual Manager Manager { get; set; } = null!;
        public virtual ICollection<Stock> Stocks { get; set; } = new HashSet<Stock>();
    }
}