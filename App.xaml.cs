using System.Configuration;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Windows;

namespace Code_editor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public async Task ExecuteCodeAsync()
        {
            using (var client = new HttpClient())
            {
                // Налаштуйте ваш запит
                var content = new StringContent("{ JSON payload }", Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://api.hackerearth.com/v4/endpoint/", content);
                var result = await response.Content.ReadAsStringAsync();

                // Обробка результату
            }
        }
    }

}
