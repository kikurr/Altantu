using Altantu.Core.Error;
using Altantu.Core.Interface;
using Altantu.Core.Model;
using Altantu.Core.Resource;
using System;
using System.Threading;
using Monitor = Altantu.Core.Model.Monitor;

namespace Altantu.Core.Service
{
    public class MonitorService
    {
        #region Constructor

        public MonitorService(IIntFunctionService sqlDataService, IIntFunctionService webAppPoolService, LogService logService)
        {
            this.SqlDataService = sqlDataService;
            this.WebAppPoolService = webAppPoolService;

            this.LogService = logService;
        }

        #endregion

        #region Methods

        public void ExecuteMonitor(Monitor monitor, Input checkInput = null)
        {
            try
            {
                // Ilumination effect when executing a monitor.

                monitor.Status = (int)Monitor.StatusValues.Processing;
                Thread.Sleep(100);

                // Gets service by function type.

                IIntFunctionService functionService = this.GetServiceByFunctionType(monitor.Function.FunctionType);
                monitor.Executed = DateTime.Now;

                // Checks function service.

                if (checkInput != null)
                {
                    functionService.GetInt(
                        monitor.Function.Server,
                        monitor.FunctionId,
                        checkInput.TimeOutSeconds.Value,
                        checkInput.Parameters);
                }

                // Executes input in function service.

                monitor.IntValue = functionService.GetInt(
                    monitor.Function.Server,
                    monitor.FunctionId,
                    monitor.TimeOutSeconds.Value,
                    monitor.Input.Parameters);
            }
            catch (Exception ex)
            {
                monitor.Status = (int)Monitor.StatusValues.Error;
            }
            finally
            {
                monitor.Recalculate();

                this.LogService.WriteLog(string.Format(StringResource.LogMonitoringFormat, monitor.Id, Enum.Parse(typeof(Monitor.StatusValues), monitor.Status.ToString())));
            }
        }

        private IIntFunctionService GetServiceByFunctionType(string functionType)
        {
            IIntFunctionService functionService;

            switch (Enum.Parse(typeof(Function.FunctionTypes), functionType))
            {
                case Function.FunctionTypes.SqlData:
                    functionService = this.SqlDataService;
                    break;
                case Function.FunctionTypes.WebAppPool:
                    functionService = this.WebAppPoolService;
                    break;
                default:
                    throw new ManagedException(string.Format("Invalid function type: '{0}'.", functionType));
            }

            return functionService;
        }

        #endregion

        #region Properties

        private IIntFunctionService SqlDataService { get; set; }
        private IIntFunctionService WebAppPoolService { get; set; }
        private LogService LogService { get; set; }

        #endregion
    }
}
