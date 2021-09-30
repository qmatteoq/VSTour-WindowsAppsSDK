using Microsoft.Windows.AppLifecycle;
using System.Diagnostics;
using System.Windows;

namespace FileTypeSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string FilePath { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            if (string.IsNullOrEmpty(FilePath))
            {
                txtFilePath.Text = FilePath;
            }
        }

        private void OnUnregister(object sender, RoutedEventArgs e)
        {
            ActivationRegistrationManager.UnregisterForFileTypeActivation(new string[] { ".foo", ".fee" }, Process.GetCurrentProcess().MainModule.FileName);
        }

        private void OnRegister(object sender, RoutedEventArgs e)
        {
            ActivationRegistrationManager.RegisterForFileTypeActivation(new string[] { ".foo", ".fee" }, 
                string.Empty, "File Type Sample", 
                new string[] { "View" }, 
                Process.GetCurrentProcess().MainModule.FileName);
        }
    }
}
