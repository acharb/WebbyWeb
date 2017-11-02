using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
namespace WebbyWeb.Models
{
    public class Habit
    {
        [Required]
        public string Name { get; set; }                                                            
        public string Time { get; set; }
        public string Description { get; set; }
        

    }
}
