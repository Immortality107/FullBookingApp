using System.ComponentModel.DataAnnotations;

    public class PaymentMethods
    {
        [Required]
        [Key]
        public Guid PaymentId { get; set; }

        [Required]
        [StringLength(50)]
        public required string PaymentName { get; set; }

    [Required]
    [StringLength(300)]
    public required string PaymentDetails { get; set; }


    public string? Location { get; set; }

}
