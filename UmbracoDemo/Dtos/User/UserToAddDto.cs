﻿using System.ComponentModel.DataAnnotations;

namespace UmbracoDemo.Dtos.User
{
    public class UserToAddDto
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; } = null!;
        
        [Display(Name = "User Name")]
        public string? Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(256)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = null!;


    }
}
