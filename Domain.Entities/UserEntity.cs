using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class UserEntity
    {
        [Required]
        [Display(Name = " UserModel Id")]
        public string id { get; set; }
        public string name { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string passwd { get; set; }

        [DataType(DataType.Password)]
        [Display(Name ="Confirm password")]
        [Compare("passwd",ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPasswd { get; set; }
    }
}
