namespace RetailerStores.Domain
{
    public class Manager : BaseEntity
    {
        /// <summary>
        /// First name
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Last name
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Email address
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Manager category
        /// </summary>
        public int Category { get; set; }

        public virtual Store Store { get; set; } = null!;
    }
}