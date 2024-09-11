using Code_editor.Libraries;
using System.Windows;
using System.Windows.Controls;
using Code_editor.Services;
using Code_editor.Models;
using System.Net.Http;
using System.Windows.Input;
using System.Windows.Media;

namespace Code_editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isInitializing = true;
        private readonly HttpService _httpService;
        private readonly RequestBody _requestBody;

        public MainWindow()
        {
            HttpClient httpClient = new HttpClient();
            _httpService = new HttpService(httpClient); 

            _requestBody = new RequestBody(string.Empty, string.Empty, 0, 0, string.Empty, string.Empty);
            


            StylesList stylesList = new StylesList();

            LanguagesDictionary languagesDictionary = new LanguagesDictionary();


            InitializeComponent();
            styleBox.SelectedItem = "light";
            isInitializing = false;




            DropBoxLanguage.ItemsSource = languagesDictionary.GetLanguagesList();
            DropBoxLanguage.SelectedItem = languagesDictionary.GetLanguagesList().FirstOrDefault();


            DropBoxLanguage.SelectionChanged += DropBoxLanguage_SelectionChanged;
            styleBox.SelectionChanged += ThemeChange;
            styleBox.ItemsSource = stylesList.GetStyleList();

        }
        private void ThemeChange(object sender, SelectionChangedEventArgs e)
        {
            string style = styleBox.SelectedItem as string;
            var uri = new Uri(style + ".xaml", UriKind.Relative);
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
        }

        private void DropBoxLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isInitializing)
                return;
            string selectedLanguage = DropBoxLanguage.SelectedItem as string;

            if (!string.IsNullOrEmpty(selectedLanguage))
            {
                isInitializing = true;
            }
        }

        private void EditorBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!isInitializing)
            {
                _requestBody.source = EditorBox.Text;
            }

        }

        private void LogBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void RunAndTests_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string selectedLanguage = DropBoxLanguage.SelectedItem as string;
                if (!string.IsNullOrEmpty(selectedLanguage))
                {
                    var languagesDictionary = new LanguagesDictionary();
                    string languageCode = languagesDictionary.GetLanguageCode(selectedLanguage);

                    if (languageCode == null)
                    {
                        MessageBox.Show($"lang '{selectedLanguage}' not found in the dictionary.");
                        return;
                    }
                   
                    _requestBody.source = EditorBox.Text;
                    _requestBody.MemoryLimit = 256 * 1024;
                    _requestBody.TimeLimit = 5;
                    _requestBody.lang = languageCode;

                    var result = await _httpService.SendCodeAsync(languageCode, _requestBody.source, _requestBody.MemoryLimit, _requestBody.TimeLimit);
                    MessageBox.Show(result.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        #region Adding Function
        private void Minimize_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized; 
        }

        

      

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

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
        #endregion
    }
}