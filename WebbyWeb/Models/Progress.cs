using System;
namespace WebbyWeb.Models
{
    public class Progress
    {
        string ProfileName { get; set; }
        int[] WeeklyProgress { get; set; }
        int[] MonthlyProgress { get; set; }
        int DayTracker { get; set; }
    }
}
