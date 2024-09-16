using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Code_editor.ViewModels;

namespace Code_editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
           

            InitializeComponent();
            DataContext = new MainViewModel();
           

        }
        
        private void Minimize_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void Minimize_MouseLeave(object sender, MouseEventArgs e)
        {

        }


        private void Navbar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Close_MouseEnter(object sender, MouseEventArgs e)
        {
            Close.Background = new SolidColorBrush(Color.FromRgb(224, 67, 67));
        }

        private void Close_MouseLeave(object sender, MouseEventArgs e)
        {
            Close.Background = new SolidColorBrush(Color.FromRgb(199, 80, 80));
        }

        private void Close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MessageBoxResult key = MessageBox.Show("Are you sure you want to quit", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);//TODO , check if it is saved then show the messg accrdngly
            if (key == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                try
                {
                    Application.Current.Shutdown();
                }
                catch (Exception) { }
            }
        }

        private void Minimize_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}