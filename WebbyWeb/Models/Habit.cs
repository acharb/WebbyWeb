using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebbyWeb.Models
{
    public class Habit
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }                                                            
        public TimeSpan Time { get; set; }
        public string Description { get; set; }
        

    }
}
