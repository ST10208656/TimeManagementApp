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

namespace TimeManagementApp
{
    /// <summary>
    /// Interaction logic for CaptureModuleControl.xaml
    /// </summary>
    public partial class CaptureModuleControl : UserControl 
    {
        private ObservableCollection<Semester> Semesters { get; set; }
        public CaptureModuleControl(ObservableCollection<Semester> semesters)
        {
            InitializeComponent();

            Semesters = new ObservableCollection<Semester>();
            Semesters = semesters;

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

            Semester currentSemester = new Semester();

            // Get the module details from the input fields

            string moduleCodeText = moduleCodeTextBox.Text;
            string moduleNameText = moduleNameTextBox.Text;
            string numberOfCreditsText = numberOfCreditsTextBox.Text;
            int numberOfCredits = int.Parse(numberOfCreditsText);
            string classHoursPerWeekText = classHoursPerWeekTextBox.Text;
            int classHoursPerWeek = int.Parse(classHoursPerWeekText);
            currentSemester.StartDate = dateStartDate.SelectedDate.Value;
            string numberOfWeeksText = numberOfWeeksTextBox.Text;
            int numberOfWeeks = int.Parse(numberOfWeeksText);
            int selfStudyHoursPerWeek = SelfStudyHoursPerWeek(classHoursPerWeek, numberOfCredits, numberOfWeeks);
            currentSemester.NumberOfWeeks = numberOfWeeks;
            Module Module = new Module(moduleCodeText, moduleNameText, numberOfCredits, classHoursPerWeek, selfStudyHoursPerWeek);
            currentSemester.Modules.Add(Module);

            Semesters.Add(currentSemester);
            ModulelistBox.ItemsSource = currentSemester.Modules;

            moduleCodeTextBox.Text = string.Empty;
            moduleNameTextBox.Text = string.Empty;
            numberOfCreditsTextBox.Text = string.Empty;
            classHoursPerWeekTextBox.Text = string.Empty;
            numberOfWeeksTextBox.Text = string.Empty;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            /*Window2 obj = new Window2(Semesters);
            obj.Show();*/
            var mainFrame = Application.Current.MainWindow.FindName("MainFrame") as Frame;
            if (mainFrame != null)
            {
                mainFrame.Navigate(new ViewModule(Semesters));
            }


        }
        public static int SelfStudyHoursPerWeek(int classHoursPerWeek, int numberOfCredits, int numberOfWeeks)
        {

            int selfStudyHoursPerWeek = numberOfCredits * 10 / numberOfWeeks - classHoursPerWeek;

            return selfStudyHoursPerWeek;

        }
    }
}

