﻿using System.ComponentModel.DataAnnotations;

namespace Talabat.Api.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
         public string Password { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}