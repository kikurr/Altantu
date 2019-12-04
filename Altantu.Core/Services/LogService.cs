using Altantu.Core.Interfaces;
using Altantu.Core.Resources;
using System;

namespace Altantu.Core.Services
{
    public class LogService
    {
        #region Values

        public enum LogMessageCodes
        {
            Info,
            Debug,
            Sql,
            Warning,
            Error,
        }

        #endregion

        #region Constructors

        public LogService(string appName, ITextFileService textFileService)
        {
            this.AppName = appName;
            this.TextFileService = textFileService;
        }

        #endregion

        #region Methods

        public void WriteLog(string message = null, LogMessageCodes code = LogMessageCodes.Info)
        {
            message = string.Format(StringResource.LogMessageFormat, DateTime.Now, code, message);
            this.WriteLine(message);
        }

        public void WriteLogSeparator()
        {
            this.WriteLine();
        }

        private void WriteLine(string message = null)
        {
            // TODO: Check if log is enabled

            try
            {
                var logFileName = string.Format(StringResource.LogFileNameFormat, DateTime.Today, this.AppName);
                this.TextFileService.WriteLine(logFileName, message);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Properties

        private string AppName { get; set; }

        private ITextFileService TextFileService { get; set; }

        #endregion
    }
}
