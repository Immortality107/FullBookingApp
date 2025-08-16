using System.ComponentModel.DataAnnotations;

    public class Review
    {
        [Required]
        [Key]
        public Guid ReviewId { get; set; }

        [Required]
        public Guid BookingID { get; set; }

        [Required]
        public required string ReviewMessage { get; set; }

        public Booking? booking { get; set; }

    }
