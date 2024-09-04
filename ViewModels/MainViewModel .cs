using System.Windows;

namespace Code_editor.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string _code;
        public string Code
        {
            get => _code;
            set
            {
                _code = value;
                OnPropertyChanged();
            }
        }
        

       
    }
}
