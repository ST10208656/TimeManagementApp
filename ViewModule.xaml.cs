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
    /// Interaction logic for ViewModule.xaml
    /// </summary>

    public partial class ViewModule : UserControl //User control used to view the list of modules and the study hours per week
    {
        private ObservableCollection<Semester> Semesters { get; set; }
        private Semester currentSemester;
        public ViewModule(Semester currentSemester1, ObservableCollection<Semester> semesters)
        {
            InitializeComponent();
            Semesters = semesters; //Sets the reference for the semester list in the previous user control
            currentSemester = currentSemester1; //Sets the reference for the current semester instance in the previous user control

            var allModules = Semesters.SelectMany(semester => semester.Modules);
            var date = Semesters.Select(semester => semester.NumberOfWeeks);

            ModuleNameListBox.ItemsSource = allModules; // Bind the ListBox to the list of modules

            if (ModuleNameListBox.Items.Count == 0) //If there is not items in the listbox some controls will be hidden or visible
            {
                label2.Visibility = Visibility.Visible;//This will be visible to show the appropriate message when there is not modules captured
                ModuleNameListBox.Visibility = Visibility.Hidden;//It will hide the list box so that the content wont be covered


            }
            else if (ModuleNameListBox.Items != null)//Else the content will still be visible if there are modules captured
            {

                ModuleNameListBox.Visibility = Visibility.Visible;

            }

        }
        private void ModuleNameListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check if an item is selected
            if (ModuleNameListBox.SelectedItem is Module selectedModule)
            {
                var mainFrame = Application.Current.MainWindow.FindName("MainFrame") as Frame;
                if (mainFrame != null)
                {
                    // Create an instance of ModuleDetailsControl with selectedModule and the semester list as a parameter
                    ModuleDetailsControl moduleDetailsControl = new ModuleDetailsControl(selectedModule, Semesters);

                    // Set the DataContext of the ModuleDetailsControl to the selectedModule
                    moduleDetailsControl.DataContext = selectedModule;

                    // Navigate to the ModuleDetailsControl
                    mainFrame.Navigate(moduleDetailsControl);
                }
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var mainFrame = Application.Current.MainWindow.FindName("MainFrame") as Frame; //This line of code fetches the main window that contains the main frame
            if (mainFrame != null)
            {
                mainFrame.Navigate(new CaptureModuleControl(currentSemester, Semesters)); //Goes to the next user control by changing the main frame's content
            }
        }
    }
}
