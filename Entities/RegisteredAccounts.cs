using System.ComponentModel.DataAnnotations;
using System.Data;

public class RegisteredAccounts
    {
       [Required]
       [Key]
       public Guid AccountID { get; set; }
        [Required]
        [StringLength(75)]
   
        public required string Email { get; set; }

       [Required]
       [StringLength(75)]
       public required string UserName { get; set; }

    [Required]
        [MaxLength(200)]
        public required string HashedPassword { get; set; }



}
