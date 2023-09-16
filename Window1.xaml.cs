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
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;

namespace TimeManagementApp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
     
        private ObservableCollection<Semester> Semesters { get; set; }
        public Window1(ObservableCollection<Semester> semesters)
        {
            InitializeComponent();

            Semesters = new ObservableCollection<Semester>();
            
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
            ModulelistBox.ItemsSource = null;
            ModulelistBox.ItemsSource = currentSemester.Modules;

            

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window2 obj = new Window2(Semesters);
            obj.Show();
        }
        public static int SelfStudyHoursPerWeek(int classHoursPerWeek, int numberOfCredits, int numberOfWeeks){

           int selfStudyHoursPerWeek = numberOfCredits * 10 / numberOfWeeks - classHoursPerWeek;

            return selfStudyHoursPerWeek;
            
            }
    }
}
