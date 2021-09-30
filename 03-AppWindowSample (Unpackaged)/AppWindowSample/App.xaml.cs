using Microsoft.Windows.ApplicationModel;
using System.Windows;

namespace AppWindowSample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            MddBootstrap.Initialize(0x00010000, "preview1");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            MddBootstrap.Shutdown();
        }
    }
}
