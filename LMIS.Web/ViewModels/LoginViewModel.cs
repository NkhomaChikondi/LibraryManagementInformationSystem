﻿using System.ComponentModel.DataAnnotations;

namespace LMIS.Web.ViewModels
{
    public class LoginViewModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
