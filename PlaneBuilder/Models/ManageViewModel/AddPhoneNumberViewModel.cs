using System;
using System.ComponentModel.DataAnnotations;

namespace PlaneBuilder.Models.ManageViewModel
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
    }
}
