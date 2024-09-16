using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Code_editor.ViewModels;

namespace Code_editor.Models
{
    [Serializable]
    public class RequestBody : ViewModelBase
    {
        private string _source;
        [JsonProperty("source")]
        [Required]
        public string Source
        {
            get => _source;
            set
            {
                if (_source != value)
                {
                    _source = value;
                    OnPropertyChanged(nameof(Source));
                }
            }
        }

        private string _lang;
        [JsonProperty("lang")]
        [Required]
        public string Lang
        {
            get => _lang;
            set
            {
                if (_lang != value)
                {
                    _lang = value;
                    OnPropertyChanged(nameof(Lang));
                }
            }
        }

        private string? _input;
        [JsonProperty("input")]
        public string? Input
        {
            get => _input;
            set
            {
                if (_input != value)
                {
                    _input = value;
                    OnPropertyChanged(nameof(Input));
                }
            }
        }

        private int _memoryLimit;
        [JsonProperty("memory_limit")]
        public int MemoryLimit
        {
            get => _memoryLimit;
            set
            {
                if (_memoryLimit != value)
                {
                    _memoryLimit = value;
                    OnPropertyChanged(nameof(MemoryLimit));
                }
            }
        }

        private int _timeLimit;
        [JsonProperty("time_limit")]
        public int TimeLimit
        {
            get => _timeLimit;
            set
            {
                if (_timeLimit != value)
                {
                    _timeLimit = value;
                    OnPropertyChanged(nameof(TimeLimit));
                }
            }
        }

        private string? _context;
        [JsonProperty("context")]
        public string? Context
        {
            get => _context;
            set
            {
                if (_context != value)
                {
                    _context = value;
                    OnPropertyChanged(nameof(Context));
                }
            }
        }

        private string? _callBack;
        [JsonProperty("callback")]
        public string? CallBack
        {
            get => _callBack;
            set
            {
                if (_callBack != value)
                {
                    _callBack = value;
                    OnPropertyChanged(nameof(CallBack));
                }
            }
        }

        // Конструктор для 4 параметрів
        public RequestBody(string source, string lang, int memoryLimit, int timeLimit)
        {
            Source = source;
            Lang = lang;
            MemoryLimit = memoryLimit;
            TimeLimit = timeLimit;
        }
    }
}
