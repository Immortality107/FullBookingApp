using Azure.Core;
using Entities;
using ServiceContracts.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

public class SignUpDTO
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email format")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Confirm Password is required")]
    [Compare("Password", ErrorMessage = "Password and Confirm Password do not match")]
    public string? ConfirmPassword { get; set; }

    [Required(ErrorMessage = "Username is required")]

    public string? UserName { get; set; }

    [Required(ErrorMessage = "Choose Country")]

    public string? Country { get; set; }

    [Required(ErrorMessage = "Age is required")]
    //[MaxLength(90,ErrorMessage = "Age Can Not Be More than 90")]
    
    public int Age { get; set; }

    [Required(ErrorMessage = "Add Valid Phone")]
    [Phone]
    public string? Phone { get; set; }

    [Required(ErrorMessage = "Choose Your Gender")]
    public string? Gender { get; set; }

    public ClientDTO ToClientDTO(SignUpDTO signUpDTO)
    {
        return new ClientDTO
        {
            Age= signUpDTO.Age,
            ClientName=signUpDTO.UserName,
            Country = signUpDTO.Country,
            Email=signUpDTO.Email,
            Gender= signUpDTO.Gender,
            Phone = signUpDTO.Phone
        };
    }
}
    
