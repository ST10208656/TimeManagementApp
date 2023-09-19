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
    public partial class CaptureModuleControl : UserControl 
    {
        private ObservableCollection<Semester> Semesters { get; set; }
        private Semester currentSemester;
        public CaptureModuleControl(Semester currentSemester1,ObservableCollection<Semester> semesters)
        {
            InitializeComponent();

            Semesters = new ObservableCollection<Semester>();
            Semesters = semesters;
            currentSemester = currentSemester1;

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

           
            try
            {
                // Get the module details from the input fields

                string moduleCodeText = moduleCodeTextBox.Text;
                string moduleNameText = moduleNameTextBox.Text;
                string numberOfCreditsText = numberOfCreditsTextBox.Text;
                int numberOfCredits = int.Parse(numberOfCreditsText);
                if (!int.TryParse(numberOfCreditsText, out numberOfCredits))
                {
                    numberOfCredits = 0; // Change this to an appropriate default value.
                }
                string classHoursPerWeekText = classHoursPerWeekTextBox.Text;
                int classHoursPerWeek = int.Parse(classHoursPerWeekText);
                if (!int.TryParse(classHoursPerWeekText, out classHoursPerWeek))
                {
                    // Handle invalid input for classHoursPerWeek (not a valid integer)
                    // You can display an error message or log the error.
                    // For now, I'll set a default value, but you should handle this according to your application's requirements.
                    classHoursPerWeek = 0; // Change this to an appropriate default value.
                }
                int selfStudyHoursPerWeek = ModuleManager.SelfStudyHoursPerWeek(classHoursPerWeek, numberOfCredits, currentSemester.NumberOfWeeks);

                
                Module Module = new Module(moduleCodeText, moduleNameText, numberOfCredits, classHoursPerWeek, selfStudyHoursPerWeek);
                currentSemester.Modules.Add(Module);

                if (selfStudyHoursPerWeek <= 0)
                {
                    MessageBox.Show("Negative value found, please re-check what you entered");
                    currentSemester.Modules.Remove(Module);
                }
                ModulelistBox.ItemsSource = currentSemester.Modules;

                moduleCodeTextBox.Text = string.Empty;
                moduleNameTextBox.Text = string.Empty;
                numberOfCreditsTextBox.Text = string.Empty;
                classHoursPerWeekTextBox.Text = string.Empty;
            }
            catch (Exception ex)
            {
MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            /*Window2 obj = new Window2(Semesters);
            obj.Show();*/
            var mainFrame = Application.Current.MainWindow.FindName("MainFrame") as Frame;
            if (mainFrame != null)
            {
                mainFrame.Navigate(new ViewModule(currentSemester, Semesters));
            }


        }
       
    }
}

