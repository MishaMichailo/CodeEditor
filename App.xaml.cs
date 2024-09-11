using Code_editor.Interfaces;
using Code_editor.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Code_editor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {

                    ConfigureServices(services);
                })
                .Build();

            this.ShutdownMode = ShutdownMode.OnLastWindowClose;
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient<IHttpService, HttpService>();
            services.AddSingleton<MainWindow>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StopAsync();
           _host.Services.GetRequiredService<MainWindow>();
            base.OnStartup(e);
        }
        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose(); 
            base.OnExit(e);
        }
    }

}
