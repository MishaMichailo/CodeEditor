using Code_editor.Models;

namespace Code_editor.Interfaces
{
    public interface IHttpService
    {
        Task<ResponseBody> SendCodeAsync(string language, string sourceCode, int memoryLimit, double timeLimit);
      //  Task<string> PollCompilationStatus(string statusUpdateUrl);
    }
}
