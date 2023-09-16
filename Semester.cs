using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagementApp
{
    public class Semester
    {
        
        public ObservableCollection<Module> Modules { get; set; }

        public int NumberOfWeeks { get; set; }
        public DateTime StartDate { get; set; }

        public Semester()
        {
          
            Modules = new ObservableCollection<Module>();



        }
        public override string ToString()
        {
            return $"Start Date: {StartDate}\nNumber of Weeks: {NumberOfWeeks}\n---------------------------------------";
        }
    }
}
