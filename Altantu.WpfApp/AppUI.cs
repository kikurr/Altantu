using Altantu.WpfApp.Windows;

namespace Altantu.WpfApp
{
    public partial class App
    {
        #region Methods

        private void LoadMainWindow()
        {
            this.AltantuMainWindow = new MainWindow(this);

            //this.MainWindow.StartStopButtonClicked += this.MainWindow_StartStopButtonClicked;
            //this.MainWindow.ClearButtonClicked += this.MainWindow_ClearButtonClicked;

            //this.IsMonitoringStarted = false;

            //this.MainWindow.Show();
        }

        #endregion

        #region Properties

        //private NotifyIcon TrayIcon { get; set; }
        private MainWindow AltantuMainWindow { get; set; }

        #endregion
    }
}
