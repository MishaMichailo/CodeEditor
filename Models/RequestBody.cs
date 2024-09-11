using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Code_editor.Models
{
    [Serializable]
    public class RequestBody
    {
        [JsonProperty("source")]
        [Required]
        public string source { get; set; }
     

        [JsonProperty("lang")]
        [Required]
        public string lang { get; set; }

        [JsonProperty("input")]
        public string? Input { get; set; }

        [JsonProperty("memory_limit")]
        public int MemoryLimit { get; set; }

        [JsonProperty("time_limit")]
        public int TimeLimit { get; set; }

        [JsonProperty("context")]
        public string? Context { get; set; }
        [JsonProperty("callback")]
        public string? CallBack { get; set; }

        public RequestBody(string sources, string langs, int memory_limit, int time_limit, string? input = null, string? context = null, string? callBack = null)
        {
            source = sources;
            lang = langs;
            MemoryLimit = memory_limit;
            TimeLimit = time_limit;
            Input = input;
            Context = context;
            CallBack = callBack;
        }
    }
}

