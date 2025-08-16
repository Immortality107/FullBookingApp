using System.ComponentModel.DataAnnotations;

    public class RegisteredAccounts
    {
       [Required]
       [Key]
       public Guid AccountID { get; set; }

        [Required]
        public Guid ClientID { get; set; }

        [Required]
        [StringLength(75)]
        public required string Email { get; set; }

        [Required]
        [StringLength(25)]
        public required string Password { get; set; }

        public  bool RememberPassword { get; set; }

    }
