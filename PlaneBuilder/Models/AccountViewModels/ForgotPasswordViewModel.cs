using System;
using System.ComponentModel.DataAnnotations;

namespace PlaneBuilder.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
