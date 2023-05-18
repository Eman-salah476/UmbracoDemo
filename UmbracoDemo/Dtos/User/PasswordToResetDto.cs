using System.ComponentModel.DataAnnotations;

namespace UmbracoDemo.Dtos.User
{
    public class PasswordToResetDto
    {
        [Required]
        [Display(Name = "User Name")]
        public string Username { get; set; } = null!;

        [Required]
        [StringLength(256)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; } = null!;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
