using System;
using System.ComponentModel.DataAnnotations;
namespace WebbyWeb.Models
{
    public class RegistrationViewModel
    {
        [Required,MaxLength(20),Display(Name = "userName")]
        public string UserName { get; set; }

        [Required,MinLength(6),MaxLength(30),DataType(DataType.Password),Display(Name="password")]
        public string Password { get; set; }

        [Required, MinLength(6), MaxLength(30), DataType(DataType.Password), Display(Name = "confirm password")]
        [Compare("Password", ErrorMessage = "confirm password should match password")]
        public string ConfirmPassword { get; set; }

    }
}
