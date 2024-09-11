using Code_editor.Models;

namespace Code_editor.Interfaces
{
    public interface IHttpService
    {
        Task<RequestBody> SendCodeAsync(string language, string sourceCode, int memoryLimit, int timeLimit);
    }
}
