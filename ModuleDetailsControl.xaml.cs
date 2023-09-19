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
    public partial class ModuleDetailsControl : UserControl //User control used to view details of the module and to add the hours studied
    {

        private ObservableCollection<Semester> Semesters;
        private CapturedHour capturedHours; //Creates instance for the hours users studied

        private Module module;
        public ModuleDetailsControl(Module module, ObservableCollection<Semester> semesters)
        {
            InitializeComponent();
            this.module = module; //Sets the reference for the module instance in the previous user control

            Semesters = semesters; //Sets the reference for the semester list in the previous user control

        }

        private void CaptureHoursButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                int hoursStudied = int.Parse(HoursStudiedTextBox.Text);
                DateTime date = DateStudiedDatePicker.SelectedDate.Value; //This will use the date the user selectd and it will be assigned to the date that the user studied
                capturedHours = new CapturedHour(hoursStudied, date); //Creates the instance for the studied hours



                module.SelfStudyHoursPerWeek = ModuleManager.RemainingHours(hoursStudied, module.SelfStudyHoursPerWeek); //Uses the remaining hours calculated in the module manager and updates th study hours per week
                Semester semesterOfModule = Semesters.FirstOrDefault(semester => semester.Modules.Contains(module)); //lINQ query used to fetch the first value in the semesters list then adds the captured hours to the module
                                                                                                                     // Update the UI or perform any other necessary actions

                if (semesterOfModule != null)
                {
                    // Add the captured hours to the ObservableCollection in the semester
                    module.CapturedHours.Add(capturedHours);
                    CapturedHoursListView.DataContext = module;

                }
                if (module.SelfStudyHoursPerWeek <= 0)
                {
                    MessageBox.Show("You have successfully reached study hours");//Once the recommended amount of hours have been used the appropriate message displays
                }

                StudyHoursTextBlock.Text = "Remaining Study Hours Per Week: " + module.SelfStudyHoursPerWeek.ToString(); //This shows the remaining study hours before the list view and it updates as the user adds hours

                // Clears the input fields:
                HoursStudiedTextBox.Clear();
                DateStudiedDatePicker.SelectedDate = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            var mainFrame = Application.Current.MainWindow.FindName("MainFrame") as Frame; //This line of code fetches the main window that contains the main frame
            if (mainFrame != null)
            {
                mainFrame.Navigate(new WelcomePage()); //Goes to the welcome page user control by changing the main frame's content
            }
        }
    }
}

