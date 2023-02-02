using System.Threading;
using System.Windows;

namespace XSLT.Viewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Mutex _instanceMutex;

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                _instanceMutex = new Mutex(true, @"Global\ViewerMutex", out var createdNew);
                if (!createdNew)
                {
                    _instanceMutex = null;
                    Current.Shutdown();
                    return;
                }
            }
            catch
            {
                //ignore
            }

            base.OnStartup(e);
            ComposeObjects();
            Current.MainWindow?.Show();
        }

        private static void ComposeObjects()
        {
            Current.MainWindow = Container.Get<MainWindow>();
        }
    }
}
