using Altantu.Core.Entities;
using Altantu.Core.Exceptions;
using Altantu.Core.Interfaces;
using Altantu.Core.Resources;
using System.IO;

namespace Altantu.Core.Services
{
    public class ConfigurationService
    {
        #region Constructors

        public ConfigurationService(string appName, IXmlFileService xmlFileService)
        {
            this.AppName = appName;
            this.ConfigurationFileName = string.Format(StringResource.ConfigurationFileNameFormat, this.AppName);
            this.XmlFileService = xmlFileService;
        }

        #endregion

        #region Methods

        public Configuration GetConfiguration()
        {
            Configuration configuration;

            try
            {
                configuration = this.XmlFileService.GetItem<Configuration>(this.ConfigurationFileName);
                configuration.Initialize();
            }
            catch (DirectoryNotFoundException)
            {
                throw new MyException(string.Format("Missing configuration file {0}.", this.ConfigurationFileName));
            }
            catch (FileNotFoundException)
            {
                throw new MyException(string.Format("Missing configuration file {0}.", this.ConfigurationFileName));
            }
            catch
            {
                throw;
            }

            return configuration;
        }

        public void SaveConfiguration(Configuration configuration)
        {
            this.XmlFileService.SaveItem<Configuration>(this.ConfigurationFileName, configuration);
        }

        #endregion

        #region Properties

        private string AppName { get; set; }

        private string ConfigurationFileName { get; set; }

        private IXmlFileService XmlFileService { get; set; }

        #endregion
    }
}
