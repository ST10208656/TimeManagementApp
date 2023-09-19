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
    /// Interaction logic for WelcomePage.xaml
    /// </summary>
    public partial class WelcomePage : UserControl
    {
        private ObservableCollection<Semester> semesters;
        public WelcomePage()
        {
            InitializeComponent();
            semesters = new ObservableCollection<Semester>();
        }

    


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var mainFrame = Application.Current.MainWindow.FindName("MainFrame") as Frame;
            if (mainFrame != null)
            {
                mainFrame.Navigate(new CaptureSemesterControl());
            }
        }
    }
}
