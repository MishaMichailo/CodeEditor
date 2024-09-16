using Newtonsoft.Json;

namespace Code_editor.Models
{
    public class ResponseBody
    {
        [JsonProperty("context")]
        public string Context { get; set; }

        [JsonProperty("result")]
        public Result Result { get; set; }

        [JsonProperty("he_id")]
        public string HeId { get; set; }

        [JsonProperty("status_update_url")]
        public string StatusUpdateUrl { get; set; }

        [JsonProperty("request_status")]
        public RequestStatus RequestStatus { get; set; }

        public override string ToString()
        {
            return $"Context: {Context}, HeId: {HeId}, StatusUpdateUrl: {StatusUpdateUrl}, RequestStatus: [{RequestStatus}]";
        }
    }

    public class Result
    {
        [JsonProperty("run_status")]
        public RunStatus RunStatus { get; set; }

        [JsonProperty("compile_status")]
        public string CompileStatus { get; set; }

        public override string ToString()
        {
            return $"RunStatus: {RunStatus}, CompileStatus: {CompileStatus}";
        }
    }

    public class RunStatus
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("output")]
        public string Output { get; set; }

        public override string ToString()
        {
            return $"Status: {Status},Output: {Output}";
        }
    }

    public class RequestStatus
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        public override string ToString()
        {
            return $"Code: {Code}, Message: {Message}";
        }
    }
}
