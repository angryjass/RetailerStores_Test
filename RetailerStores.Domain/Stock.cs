namespace RetailerStores.Domain
{
    public class Stock : BaseEntity
    {
        /// <summary>
        /// Back stock
        /// </summary>
        public int Backstock { get; set; }

        /// <summary>
        /// Front stock
        /// </summary>
        public int Frontstock { get; set; }

        /// <summary>
        /// Shopping window
        /// </summary>
        public int ShoppingWindow { get; set; }

        /// <summary>
        /// Accuracy
        /// </summary>
        public decimal Accuracy { get; set; }

        /// <summary>
        /// On-Floor-Availability
        /// </summary>
        public decimal OnFloorAvailability { get; set; }

        /// <summary>
        /// Mean age
        /// </summary>
        public int MeanAge { get; set; }

        /// <summary>
        /// Date of record stock info
        /// </summary>
        public DateTimeOffset RecordingDate { get; set; }

        /// <summary>
        /// Store identifier
        /// </summary>
        public Guid StoreId { get; set; }

        public virtual Store Store { get; set; } = null!;
    }
}