using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebbyWeb.Models
{
    public class Profile
    {
        public int ID { get; set; }
        [Required(ErrorMessage="required.")]
        public string UserName { get; set; }
        [Required(ErrorMessage="required.")]
        public string Password { get; set; } //string of times, seperated by commas
        [Required(ErrorMessage="required.")]
        public string ConfirmPassword { get; set; }
        
        

    }
}