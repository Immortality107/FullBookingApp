using System.ComponentModel.DataAnnotations;
namespace BookingApp.Models
{
    public class Clients
    {
        [Required]
        [Key]
        public Guid ClientId { get; set; }

        [Required]
        [StringLength(25)]
        public required string ClientName { get; set; }

        [Required]
        [StringLength(20)]
        public required string Country {  get; set; }

        [Required]  
        [StringLength(50)]
        public required string Email { get; set; }

        [Required]
        [StringLength(15)]
        public required string Phone { get; set; }

        [Required]
        public  int Age { get; set; }

        [Required]
        public required string Gender { get; set; } //enGenderOptions

        public string? Notes { get; set; }

    }
}
