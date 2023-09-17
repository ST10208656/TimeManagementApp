using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagementApp
{
    public class Semester: INotifyCollectionChanged
    {
        
        public ObservableCollection<Module> Modules { get; set; }
        public ObservableCollection<CapturedHour> CapturedHours { get; set; }
        public int NumberOfWeeks { get; set; }
        public DateTime StartDate { get; set; }

        public Semester()
        {
          
            Modules = new ObservableCollection<Module>();
            CapturedHours = new ObservableCollection<CapturedHour>();
            CapturedHours.CollectionChanged += CapturedHours_CollectionChanged;


        }


        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        private void CapturedHours_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Notify subscribers about the collection change
            CollectionChanged?.Invoke(this, e);
        }
        public override string ToString()
        {
            return $"Start Date: {StartDate}\nNumber of Weeks: {NumberOfWeeks}\n---------------------------------------";
        }
    }
}
