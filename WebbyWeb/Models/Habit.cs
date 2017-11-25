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
        [Required]
        public string Time { get; set; } //string of times, seperated by commas
        [Required]
        public string Description { get; set; }
        public string DoneOrNot { get; set; }   //binary string of done or not done corresponding to time string. 0 =not done, 1 = done. eg. 00010
        public string ProfileName {get;set;}
        
        public Profile Profile {get;set;}

    }
}
