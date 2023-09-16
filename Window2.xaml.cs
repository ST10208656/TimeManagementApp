using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace TimeManagementApp
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        private ObservableCollection<Semester> Semesters { get; set; }
        public Window2(ObservableCollection<Semester> semesters)
        {
            InitializeComponent();
          Semesters = semesters;
            DataContext = semesters;
            var allModules = Semesters.SelectMany(semester => semester.Modules);
            var date = Semesters.Select(semester => semester.NumberOfWeeks);
            // Bind the ListBox to the list of modules
            ModuleNameListBox.ItemsSource = allModules;

            if (ModuleNameListBox.Items.Count == 0)
            {
                label2.Visibility = Visibility.Visible;
                ModuleNameListBox.Visibility = Visibility.Hidden;
                

            }
            else if(ModuleNameListBox.Items != null)
            {
                
            ModuleNameListBox.Visibility = Visibility.Visible;
               
            }
        }
        private void ModuleNameListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check if an item is selected
            if (ModuleNameListBox.SelectedItem is Module selectedModule)
            {
                // Get the selected module code
    
               
            
            
                    moduleDetailWindow moduleDetailWindow = new moduleDetailWindow();

                moduleDetailWindow.DataContext = selectedModule;    
                    moduleDetailWindow.ShowDialog();

                // Create a new window to display the module code as the title

                // Add any additional UI elements or content to the new window as needed
                // For example, you can create labels, text blocks, or other controls to display more details about the selected module.

               
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Window1 obj = new Window1(Semesters);
            obj.ShowDialog();
        }
    }
}
