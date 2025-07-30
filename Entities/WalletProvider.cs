using System.ComponentModel.DataAnnotations;

public class WalletPaymentViewModel
{
    [Required]
    public string WalletProvider { get; set; }

    [Required]
    [Phone]
    public string WalletNumber { get; set; }

    [Required]
    public string Name { get; set; }
}
