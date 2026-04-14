using System.Configuration;
using System.Data;
using System.Windows;

namespace Banksystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            SplashScreen splash = new SplashScreen("RSPsplash.png");
            splash.Show(false);
            Thread.Sleep(1500);
            splash.Close(TimeSpan.FromSeconds(0.5));
            base.OnStartup(e);
        }
    }

}
