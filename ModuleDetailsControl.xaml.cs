using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TimeManagementClassLibrary;

namespace TimeManagementApp
{
    /// <summary>
    /// Interaction logic for ModuleDetailsControl.xaml
    /// </summary>
    public partial class ModuleDetailsControl : UserControl
    {

        private ObservableCollection<Semester> Semesters;
        private CapturedHour capturedHours;

        private Module module;
        public ModuleDetailsControl(Module module, ObservableCollection<Semester> semesters)
        {
            InitializeComponent();
            this.module = module;

            Semesters = semesters;

        }

        private void CaptureHoursButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                int hoursStudied = int.Parse(HoursStudiedTextBox.Text);
                DateTime date = DateStudiedDatePicker.SelectedDate.Value;
                capturedHours = new CapturedHour(hoursStudied, date);

                // Subtract hours studied from self-study hours
                // module.SelfStudyHoursPerWeek -= hoursStudied;
                module.SelfStudyHoursPerWeek = ModuleManager.RemainingHours(hoursStudied, module.SelfStudyHoursPerWeek);
                Semester semesterOfModule = Semesters.FirstOrDefault(semester => semester.Modules.Contains(module));
                // Update the UI or perform any other necessary actions
                // (e.g., display the updated self-study hours)
                if (semesterOfModule != null)
                {
                    // Add the captured hours to the ObservableCollection in the semester
                    semesterOfModule.CapturedHours.Add(capturedHours);
                    CapturedHoursListView.DataContext = semesterOfModule;

                }
                if (module.SelfStudyHoursPerWeek <= 0)
                {
                    MessageBox.Show("You have successfully reached study hours");
                }

                StudyHoursTextBlock.Text = "Remaining Study Hours Per Week: " + module.SelfStudyHoursPerWeek.ToString();

                // Optionally, you can clear the input fields:
                HoursStudiedTextBox.Clear();
                DateStudiedDatePicker.SelectedDate = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
     
    }
}
