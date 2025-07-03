using System.ComponentModel.DataAnnotations;
namespace BookingApp.Models
{
    public class Bookings
    {
        [Required]
        [Key]
        public Guid BookingId { get; set; }

        [Required]  
        public required Guid ClientID { get; set; }

        [Required]
        public required Guid ServiceID {  get; set; } 

        [Required] 
        public required Guid PaymentID { get; set; }

        [Required]
        public required string Status { get; set; }  //enStatusOptions

    }
}
