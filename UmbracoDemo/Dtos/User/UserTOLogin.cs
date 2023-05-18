using System.ComponentModel.DataAnnotations;

namespace UmbracoDemo.Dtos.User
{
    public class UserTOLogin
    {
        [Required]
        [Display(Name = "User Name")]
        public string Username { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(256)]
        public string Password { get; set; } = null!;
        public bool RememberMe { get; set; } = false;
    }
}
