using System.ComponentModel.DataAnnotations;

public class Booking
{
    [Required]
    [Key]
    public Guid BookingId { get; set; }

    [Required]
    public required Guid ClientID { get; set; }

    [Required]
    public required Guid PaymentID { get; set; }

    [Required]
    public required Guid ServiceID { get; set; }

    [Required]
    public required string Status { get; set; }//enStatusOptions

    public required double Amount { get; set; }

    public Service? Service { get; set; }

    public Client? client { get; set; }

    public PaymentMethods? payment { get; set; }



}

