using System;
using System.Collections.Generic;

namespace WebbyWeb.Models
{
    public class Progress
    {
        public int ID { get; set; }
        public string ProfileName { get; set; }
        public int WeeklyProgress { get; set; }
        public int WeeklyPtsPossible {get;set;}
        public int MonthlyProgress { get; set; }
        public int MonthlyPtsPossible { get;set;}
        public int DayTracker { get; set; }
        public int NumOfHabits { get;set;}
        public DateTime DateTracker {get;set;}


    }
}
