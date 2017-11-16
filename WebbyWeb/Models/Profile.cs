using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebbyWeb.Models
{
    public class Profile
    {
        public int ID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; } //string of times, seperated by commas
        [Required]
        public string ConfirmPassword { get; set; }
        

    }
}