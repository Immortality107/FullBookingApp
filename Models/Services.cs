using System.ComponentModel.DataAnnotations;
namespace BookingApp.Models
{
    public class Services
    {
        [Required]
        [Key]
        public Guid ServiceId { get; set; }

        [Required]
        [StringLength(25)]
        public required string ServiceName { get; set; }

   

    }
}
