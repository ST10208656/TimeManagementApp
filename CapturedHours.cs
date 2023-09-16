using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagementApp
{
    public class CapturedHours
    {
        public int HoursStudied { get; set; }
        public DateTime DateStudied { get; set; }

        public CapturedHours(int hoursStudied, DateTime dateStudied)
        {
            HoursStudied= hoursStudied;
            DateStudied= dateStudied;   
        }
    }
}
