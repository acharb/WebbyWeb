using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
namespace WebbyWeb.Models
{
    public class Habit
    {
        [Required]
        public Dictionary<string,List<int>> Habits { get; set; }    //dictionary key is habit name, value is list of integers
                                                                    //list[0] is habit type, rest are times per day


    }
}
//making changes to see if it gets added