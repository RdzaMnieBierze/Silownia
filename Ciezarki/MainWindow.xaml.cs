using Ciezarki.Core;
using Ciezarki.MVVM.Viewmodel;
using System.Windows;

namespace Ciezarki
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainVM(new NavigationService());
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}