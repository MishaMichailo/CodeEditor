using Code_editor.Models;

namespace Code_editor.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private RequestBody requestBody;

        public MainViewModel()
        {
         //   requestBody = new RequestBody();
        }

        public string Language
        {
            get => requestBody.lang;
            set
            {
                requestBody.lang = value;
                OnPropertyChanged(nameof(Language));
            }
        }

    }
}
