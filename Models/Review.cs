using System.ComponentModel.DataAnnotations;
namespace BookingApp.Models
{
    public class Review
    {
        [Required]
        [Key]
        public Guid ReviewId { get; set; }

        [Required]
        public Guid BookingID {  get; set; }

        [Required] 
        public required string ReviewMessage { get; set; }


    }
}
