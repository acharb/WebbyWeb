using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebbyWeb.Models
{
    public class Profile    //principal entity to the dependent entity Habit
    {
        public int ID { get; set; }
        [Required,EmailAddress,MaxLength(256),Display(Name = "Email")]
        public string Email { get; set; }
        [Required,MinLength(6),MaxLength(30),DataType(DataType.Password),Display(Name="password")]
        public string Password { get; set; } //string of times, seperated by commas

        //public List<Habit> Habits {get;set;}


        
        

    }
}