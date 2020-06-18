using System;
using System.ComponentModel.DataAnnotations;

namespace PlaneBuilder.Models.ManageViewModel
{
    public class VerifyPasswordViewModel
    {
        [Required]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
    }
}
