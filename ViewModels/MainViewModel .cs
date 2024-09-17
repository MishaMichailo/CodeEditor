using Code_editor.Libraries;
using Code_editor.Models;
using Code_editor.Services;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;

namespace Code_editor.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly HttpService _httpService;
        private RequestBody _requestBody;
        private ObservableCollection<string> _languagesList;
        private ObservableCollection<string> _themesList;

        public ObservableCollection<string> LanguagesList
        {
            get => _languagesList;
            set => SetProperty(ref _languagesList, value);
        }

        public ObservableCollection<string> ThemesList
        {
            get => _themesList;
            set => SetProperty(ref _themesList, value);
        }

        private string _selectedLanguage;
        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
               
                if (SetProperty(ref _selectedLanguage, value))
                {

                    _requestBody.Lang = new LanguagesDictionary().GetLanguageCode(value);

                    EditorText = new LanguagesDictionary().GetInitialCodeTemplate(value);
                }
            }
        }

        private string _selectedTheme;
        public string SelectedTheme
        {
            get => _selectedTheme;
            set
            {
                if (SetProperty(ref _selectedTheme, value))
                {
                    ChangeTheme();
                }
            }
        }

        private string _editorText;
        public string EditorText
        {
            get => _editorText;
            set
            {
                if (SetProperty(ref _editorText, value))
                {
                    _requestBody.Source = value;
                }
            }
        }

        private string _outputResult;
        public string OutputResult
        {
            get => _outputResult;
            set => SetProperty(ref _outputResult, value);
        }

        private string _compileStatus;
        public string CompileStatus
        {
            get => _compileStatus;
            set => SetProperty(ref _compileStatus, value);
        }

        public ICommand SubmitCommand { get; }
        public ICommand ChangeThemeCommand { get; }


        public MainViewModel()
        {
            _httpService = new HttpService(new HttpClient());
            _requestBody = new RequestBody(string.Empty, string.Empty, 256 * 1024, 5);
            SubmitCommand = new RelayCommand(async () => await SubmitCodeAsync());
            ChangeThemeCommand = new RelayCommand(ChangeTheme);

            LanguagesList = new ObservableCollection<string>(new LanguagesDictionary().GetLanguagesList());
            ThemesList = new ObservableCollection<string>(new StylesList().GetStyleList());

            SelectedTheme = "light";
        }

        private async Task SubmitCodeAsync()
        {
            try
            {
                CompileStatus = "Compiling...";
                if (!string.IsNullOrEmpty(_requestBody.Lang))
                {
                    var result = await _httpService.SendCodeAsync(
                        _requestBody.Lang,
                        _requestBody.Source,
                        _requestBody.MemoryLimit,
                        _requestBody.TimeLimit
                    );

                    OutputResult = await _httpService.GetResultOutput(result.Result.RunStatus.Output);
                    CompileStatus = result.Result.CompileStatus;
                }
                else
                {
                    OutputResult = "Error: No language selected.";
                }
            }
            catch (Exception ex)
            {
                OutputResult = $"Error: {ex.Message}";
            }
        }

        private void ChangeTheme()
        {
            if (string.IsNullOrEmpty(SelectedTheme))
                return;

            var uri = new Uri("Themes/" + SelectedTheme + ".xaml", UriKind.Relative);
            var resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            if (resourceDict != null)
            {
                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            }
        }

    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute();

        public void Execute(object parameter) => _execute();
    }
}
