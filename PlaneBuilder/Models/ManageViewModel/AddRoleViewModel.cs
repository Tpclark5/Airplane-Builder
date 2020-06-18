using System;
using System.ComponentModel.DataAnnotations;

namespace PlaneBuilder.Models.ManageViewModel
{
    public class AddRoleViewModel
    {
        [Display(Name = "Role Name")]
        [Required]
        public string RoleName { get; set; }
    }
}
