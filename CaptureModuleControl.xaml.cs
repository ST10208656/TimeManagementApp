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
using TimeManagementApp;
using TimeManagementClassLibrary;


namespace TimeManagementApp
{
    /// <summary>
    /// Interaction logic for CaptureModuleControl.xaml
    /// </summary>
    public partial class CaptureModuleControl : UserControl //This is the user control to capture modules for the semester
    {
        private ObservableCollection<Semester> Semesters;
        private Semester currentSemester;
        public CaptureModuleControl(Semester currentSemester1, ObservableCollection<Semester> semesters)
        {
            InitializeComponent();
            Semesters = semesters; //Sets the reference for the semester list in the previous user control
            currentSemester = currentSemester1; //Sets the reference for the current semester instance in the previous user control

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                // The lines of code below gets the module details from the input fields
                string moduleCodeText = moduleCodeTextBox.Text;
                string moduleNameText = moduleNameTextBox.Text;
                string numberOfCreditsText = numberOfCreditsTextBox.Text;
                int numberOfCredits = int.Parse(numberOfCreditsText);
                string classHoursPerWeekText = classHoursPerWeekTextBox.Text;
                int classHoursPerWeek = int.Parse(classHoursPerWeekText);

                int selfStudyHoursPerWeek = ModuleManager.SelfStudyHoursPerWeek(classHoursPerWeek, numberOfCredits, currentSemester.NumberOfWeeks); //This uses a method that calculates the self study hours per week then assign it to a variable to be used in the modules list


                Module Module = new Module(moduleCodeText, moduleNameText, numberOfCredits, classHoursPerWeek, selfStudyHoursPerWeek); //Creates an instance of the module and adds it to the module list
                currentSemester.Modules.Add(Module);

                if (selfStudyHoursPerWeek <= 0) //This assures that the user enter values that will give appropriate calculations and rather not a calculation error
                {
                    MessageBox.Show("Negative value found, please re-check what you entered");
                    currentSemester.Modules.Remove(Module); //Removes the data to ensure no duplication of data when re-entering values
                }
                ModulelistBox.ItemsSource = currentSemester.Modules;//Binds the current modules entered to a list box

                MessageBox.Show("Module captured, you may enter another module or you may continue"); //Message box to instruct that the user can enter another module

                //The code below clears the text boxes for users to input values again
                moduleCodeTextBox.Text = string.Empty;
                moduleNameTextBox.Text = string.Empty;
                numberOfCreditsTextBox.Text = string.Empty;
                classHoursPerWeekTextBox.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);//Once the error has been catched it displays the apporpriate message
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var mainFrame = Application.Current.MainWindow.FindName("MainFrame") as Frame; //This line of code fetches the main window that contains the main frame
            if (mainFrame != null)
            {
                mainFrame.Navigate(new ViewModule(currentSemester, Semesters)); //Goes to the next user control by changing the main frame's content
            }


        }

    }
}

