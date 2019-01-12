using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS619Challenge.Models
{

    public enum ActivityType
    {
        Run,
        Jog,
        Cycle
    }

    public class Activity
    {
        public string UserId { get; set; }

        public int Id { get; set; }

        public ActivityType Type { get; set; }

        public DateTime Date { get; set; }

        public double Distance { get; set; }

        public int Hours { get; set; }

        public int Minutes { get; set; }

        public int Seconds { get; set; }

        public string Message { get; set; }
    }
}
