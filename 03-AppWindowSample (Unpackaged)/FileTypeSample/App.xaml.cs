using Microsoft.Windows.ApplicationModel;
using Microsoft.Windows.AppLifecycle;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Windows.ApplicationModel.Activation;

namespace FileTypeSample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            MddBootstrap.Initialize(0x00010000, "preview1");
           
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow window = new MainWindow();

            var args = AppInstance.GetCurrent().GetActivatedEventArgs();
            if (args.Kind == ExtendedActivationKind.File)
            {
                var fileActivation = args.Data as IFileActivatedEventArgs;
                string filePath = fileActivation.Files[0].Path;
                window.FilePath = filePath;
            }

            window.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            MddBootstrap.Shutdown();
        }
    }
}
