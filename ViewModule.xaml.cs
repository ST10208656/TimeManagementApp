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

    public partial class ViewModule : UserControl
    { 
        private ObservableCollection<Semester> Semesters { get; set; }
        private Semester currentSemester;
        public ViewModule(Semester currentSemester1, ObservableCollection<Semester> semesters)
        {
            InitializeComponent();
            Semesters = semesters;
            currentSemester = currentSemester1;
           
            var allModules = Semesters.SelectMany(semester => semester.Modules);
            var date = Semesters.Select(semester => semester.NumberOfWeeks);
            // Bind the ListBox to the list of modules
            ModuleNameListBox.ItemsSource = allModules;

            if (ModuleNameListBox.Items.Count == 0)
            {
                label2.Visibility = Visibility.Visible;
                ModuleNameListBox.Visibility = Visibility.Hidden;


            }
            else if (ModuleNameListBox.Items != null)
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
                    // Create an instance of ModuleDetailsControl with selectedModule as a parameter
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
            var mainFrame = Application.Current.MainWindow.FindName("MainFrame") as Frame;
            if (mainFrame != null)
            {
                mainFrame.Navigate(new CaptureModuleControl(currentSemester, Semesters));
            }
        }
    }
}
