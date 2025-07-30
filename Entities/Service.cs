using System.ComponentModel.DataAnnotations;

    public class Service
    {
        [Required]
        [Key]
        public Guid ServiceId { get; set; }

        [Required]
        [StringLength(100)]
        public required string ServiceName { get; set; }


        [Required]
        public required string ServiceDescribtion { get; set; }
     
        [Required]
        public required double ServicePriceLocal { get; set; }

       [Required]
        public required double ServicePriceInternational { get; set; }

}
