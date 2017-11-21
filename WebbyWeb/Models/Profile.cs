using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebbyWeb.Models
{
    public class Profile    //principal entity to the dependent entity Habit
    {
        public int ID { get; set; }
        [Required(ErrorMessage="required.")]
        public string UserName { get; set; }
        [Required(ErrorMessage="required.")]
        public string Password { get; set; } //string of times, seperated by commas

        public List<Habit> Habits {get;set;}
        
        

    }
}