using Code_editor.Models;

namespace Code_editor.Interfaces
{
    public interface IHttpService
    {
        Task<ResponseBody> SendCodeAsync(string language, string sourceCode, int memoryLimit, int timeLimit);
      //  Task<string> PollCompilationStatus(string statusUpdateUrl);
    }
}
