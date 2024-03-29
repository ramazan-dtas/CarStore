﻿using CarStore.Helpers;
using System.ComponentModel.DataAnnotations;

namespace CarStore.DTO.User.Request
{
    public class UpdateUser
    {
        [Required]
        [StringLength(255, ErrorMessage = "Email must be less than 255 chars")]
        public string Email { get; set; }
        [Required]
        [StringLength(255, ErrorMessage = "Username must be less than 255 chars")]
        public string Password { get; set; }
        [Required]
        public Role Role { get; set; }
    }
}
