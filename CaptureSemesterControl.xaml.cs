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
    /// Interaction logic for CaptureSemesterControl.xaml
    /// </summary>
    public partial class CaptureSemesterControl : UserControl
    {
        private Semester currentSemester;
        private ObservableCollection<Semester> semesters;
        public CaptureSemesterControl()
        {
            InitializeComponent();
            currentSemester = new Semester();
            semesters = new ObservableCollection<Semester>();
        }

        private void AddSemesterButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime? startDate = StartDatePicker.SelectedDate;

            if (!startDate.HasValue)
            {
                // Handle null or invalid date input
                // You can display an error message or log the error.
                // For now, I'll set a default value, but you should handle this according to your application's requirements.
                startDate = DateTime.Now; // Change this to an appropriate default value.
            }
            string numberOfWeeksText = NumberOfWeeksTextBox.Text;
            int numberOfWeeks = int.Parse(numberOfWeeksText);
            if (!int.TryParse(numberOfWeeksText, out numberOfWeeks))
            {
                // Handle invalid input for numberOfWeeks (not a valid integer)
                // You can display an error message or log the error.
                // For now, I'll set a default value, but you should handle this according to your application's requirements.
                numberOfWeeks = 0; // Change this to an appropriate default value.
            }
            currentSemester.StartDate = StartDatePicker.SelectedDate.Value;
            currentSemester.NumberOfWeeks = numberOfWeeks;

            semesters.Add(currentSemester);

            if (semesters.Count == 1)
            {
                maxSemesterLabel.Visibility = Visibility.Visible;
                addSemesterButton.Visibility = Visibility.Collapsed;
                captureSemesterGrid.Visibility = Visibility.Collapsed;
                instructionLabel.Visibility = Visibility.Collapsed;
            }

        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            var mainFrame = Application.Current.MainWindow.FindName("MainFrame") as Frame;
            if (mainFrame != null)
            {
                if (semesters.Count == 0)
                {
                    MessageBox.Show("Semester not captured, please enter semester details.");

                    if (mainFrame != null)
                    {
                        CaptureSemesterControl captureSemesterControl = new CaptureSemesterControl();

                        mainFrame.Navigate(captureSemesterControl);
                    }
                }
                else
                {
                    mainFrame.Navigate(new ViewModule(currentSemester, semesters));
                }

            }
           
        }

        private void NumberOfWeeksTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
