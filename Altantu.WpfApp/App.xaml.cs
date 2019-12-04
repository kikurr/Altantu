using Altantu.Core.Exceptions;
using Altantu.Core.Services;
using Altantu.External.Services;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;

namespace Altantu.WpfApp
{
    public partial class App : Application, IDisposable
    {
        #region Constructors

        public App()
        {
            this.GetApplicationNameAndVersion();
            this.CreateServices();
        }

        #endregion

        #region Methods

        private void OnLoad(string[] args)
        {
            this.ManageAppCommandLineArgs(args);

            this.LogService.WriteLog(string.Format("{0} {1} execution start", this.AppName, this.AppVersion), LogService.LogMessageCodes.Info);

            ////this.configuration = Configuration.CreateSample1_TEST();
            ////this.configurationService.SaveConfiguration(this.configuration);
            ////this.Shutdown();

            var configuration = this.ConfigurationService.GetConfiguration();

            this.LoadMainWindow();
            //this.LoadTrayIcon();

            if (configuration.AutoStart)
            {
                //this.OnStartMonitoring();
            }

            this.Shutdown();
        }

        private void GetApplicationNameAndVersion()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            this.AppName = fileVersionInfo.ProductName;

            var versionSplit = fileVersionInfo.FileVersion.Split('.');
            if (versionSplit.Length >= 2)
            {
                this.AppVersion = string.Format("{0}.{1}", versionSplit[0], versionSplit[1]);
            }

            if (string.IsNullOrWhiteSpace(this.AppName) || string.IsNullOrWhiteSpace(this.AppVersion))
            {
                throw new MyException("Invalid application name or version.");
            }
        }

        private void CreateServices()
        {
            var textFileService = new TextFileService();
            var xmlFileService = new XmlFileService();
            //var sqlDataService = new SqlDataService();
            //var webAppPoolService = new WebAppPoolService();

            this.LogService = new LogService(this.AppName, textFileService);
            this.ConfigurationService = new ConfigurationService(this.AppName, xmlFileService);
            //this.MonitorService = new MonitorService(sqlDataService, this.WebAppPoolService, this.LogService);
        }

        private void ManageAppCommandLineArgs(string[] args)
        {
        }

        private void OnException(Exception ex)
        {
            string errorMessageShort = ex.Message;
            string errorMessageLarge = ex.ToString();

            if (this.LogService != null)
            {
                // TODO: Write to log.
            }
            else
            {
                MessageBox.Show(errorMessageShort, this.AppName, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnExit()
        {
            if (this.LogService != null)
            {
                this.LogService.WriteLog(string.Format("{0} {1} execution end", this.AppName, this.AppVersion), LogService.LogMessageCodes.Info);
                this.LogService.WriteLogSeparator();
            }

            this.Dispose();
        }

        public void Dispose()
        {
            //if (this.MonitorTimers != null)
            //{
            //    foreach (var monitorTimer in this.MonitorTimers)
            //    {
            //        monitorTimer?.Dispose();
            //    }
            //}

            //this.MainTimer?.Dispose();
        }

        #endregion

        #region Event handlers

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            this.OnLoad(e.Args);
        }

        private void This_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            this.OnException(e.Exception);
            e.Handled = true;
            this.Shutdown();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            this.OnExit();
            base.OnExit(e);
        }

        #endregion

        #region Properties

        private string AppName { get; set; }

        private string AppVersion { get; set; }

        private LogService LogService { get; set; }

        private ConfigurationService ConfigurationService { get; set; }

        #endregion
    }
}
