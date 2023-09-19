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
    public partial class CaptureSemesterControl : UserControl //This is the user control to capture a semester
    {
        private Semester currentSemester; //This instance represents the semester currently being entered
        private ObservableCollection<Semester> semesters; //This instance represents the semester but in an observable collection so that it can work with the UI when needing to add and remove items
        public CaptureSemesterControl()
        {
            InitializeComponent();
            currentSemester = new Semester(); //creates new instance of the current semester 
            semesters = new ObservableCollection<Semester>(); // creates new instance for the semester list for the currentSemester object to be added to
        }

        private void AddSemesterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string numberOfWeeksText = NumberOfWeeksTextBox.Text; //The number of weeks gets fetched from the text box that represents the number of weeks
                int numberOfWeeks = int.Parse(numberOfWeeksText);

                currentSemester.StartDate = StartDatePicker.SelectedDate.Value;//The start date value gets assigned to the property 'start date' of the current semester
                currentSemester.NumberOfWeeks = numberOfWeeks;//The number of weeks value gets assigned to the property 'start date' of the current semester

                semesters.Add(currentSemester); //Once the details of the current semester are added the current semester gets added to a semester list

                if (semesters.Count == 1)//This is to ensure the semester is only entered once then the necessary content gets hidden or displayed
                {
                    maxSemesterLabel.Visibility = Visibility.Visible;
                    continueButton.Visibility= Visibility.Visible;
                    addSemesterButton.Visibility = Visibility.Collapsed;
                    captureSemesterGrid.Visibility = Visibility.Collapsed;
                    instructionLabel.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)//Error handling to catch incorrect input 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e) //The continue button is used to redirect to a different user control
        {
            var mainFrame = Application.Current.MainWindow.FindName("MainFrame") as Frame; //This line of code fetches the main window that contains the main frame
            if (mainFrame != null)
            {
                if (semesters.Count == 0) //Checks that the semester is entered or it returns to the current page so that they can re-enter the semester
                {
                    MessageBox.Show("Semester not captured, please enter semester details.");

                    if (mainFrame != null)
                    {
                        CaptureSemesterControl captureSemesterControl = new CaptureSemesterControl();//Goes back to the same user control

                        mainFrame.Navigate(captureSemesterControl);
                    }
                }
                else
                {
                    mainFrame.Navigate(new ViewModule(currentSemester, semesters));//If the semester has been entered it moves on to the next user control
                }

            }

        }

        private void NumberOfWeeksTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
