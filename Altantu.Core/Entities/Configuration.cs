using Altantu.Core.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Altantu.Core.Entities
{
    public class Configuration
    {
        #region Values

        public const int DEFAULT_TIMEOUT_SECONDS = 60;

        #endregion

        #region Methods

        public void Initialize()
        {
            this.InitializeInstances();
            this.InitializeFunctions();
            this.InitializeInputs();
            this.InitializeMonitors();
        }

        private void InitializeInstances()
        {
            if (this.Instances == null || !this.ActiveInstances.Any())
            {
                throw new MyException("No active Instances found in configuration file.");
            }
        }

        private void InitializeFunctions()
        {
            if (this.Functions != null)
            {
                foreach (var function in this.Functions)
                {
                    function.Instance = this.Instances.Single(x => x.Id == function.InstanceId);
                    this.InheritFunctionValues(function);
                }
            }

            if (this.Functions == null || !this.ActiveFunctions.Any())
            {
                throw new MyException("No active Functions found in configuration file.");
            }
        }

        private void InitializeInputs()
        {
            if (this.Inputs == null || !this.ActiveInputs.Any())
            {
                throw new MyException("No active Inputs found in configuration file.");
            }
        }

        private void InitializeMonitors()
        {
            if (this.Monitors != null)
            {
                foreach (var monitor in this.Monitors)
                {
                    monitor.Function = this.Functions.Single(x => x.Id == monitor.FunctionId);
                    monitor.Input = this.Inputs.Single(x => x.Id == monitor.InputId);
                    this.InheritMonitorValues(monitor);
                    monitor.Recalculate();
                }
            }

            if (this.Monitors == null || !this.ActiveMonitors.Any())
            {
                throw new MyException("No active Monitors found in configuration file.");
            }
        }

        public void InheritFunctionValues(Function function)
        {
            // IsActive.

            if (function.Instance.IsActive.HasValue && !function.Instance.IsActive.Value)
            {
                function.IsActive = false; // Only false value.
            }
        }

        public void InheritMonitorValues(Monitor monitor)
        {
            // IsActive.

            if (monitor.Function.IsActive.HasValue && !monitor.Function.IsActive.Value)
            {
                monitor.IsActive = false; // Only false value.
            }

            if (monitor.Input.IsActive.HasValue && !monitor.Input.IsActive.Value)
            {
                monitor.IsActive = false; // Only false value.
            }

            // TimeOutSeconds.

            if (!monitor.TimeOutSeconds.HasValue || monitor.TimeOutSeconds <= 0)
            {
                if (monitor.Input.TimeOutSeconds.HasValue && monitor.Input.TimeOutSeconds > 0)
                {
                    monitor.TimeOutSeconds = monitor.Input.TimeOutSeconds;
                }
                else
                {
                    monitor.TimeOutSeconds = DEFAULT_TIMEOUT_SECONDS;
                }
            }

            // PeriodMinutes.

            if (!monitor.PeriodMinutes.HasValue || monitor.PeriodMinutes <= 0)
            {
                if (monitor.Input.PeriodMinutes.HasValue && monitor.Input.PeriodMinutes > 0)
                {
                    monitor.PeriodMinutes = monitor.Input.PeriodMinutes;
                }
            }

            // ValueMin.

            if (!monitor.ValueMin.HasValue)
            {
                if (monitor.Input.ValueMin.HasValue)
                {
                    monitor.ValueMin = monitor.Input.ValueMin;
                }
            }

            // ValueMax.

            if (!monitor.ValueMax.HasValue)
            {
                if (monitor.Input.ValueMax.HasValue)
                {
                    monitor.ValueMax = monitor.Input.ValueMax;
                }
            }

        }

        #endregion

        #region Properties

        #region Xml properties

        public bool AutoStart { get; set; }

        public int? DefaultTimeOutSeconds { get; set; }

        public List<Instance> Instances { get; set; }

        public List<Function> Functions { get; set; }

        public List<Input> Inputs { get; set; }

        public List<Monitor> Monitors { get; set; }

        #endregion

        #region General properties

        [XmlIgnore]
        public List<Instance> ActiveInstances
        {
            get
            {
                return this.Instances.Where(x => x.IsActive.HasValue && x.IsActive.Value)
                    .ToList();
            }
        }

        [XmlIgnore]
        public List<Function> ActiveFunctions
        {
            get
            {
                return this.Functions.Where(x => x.IsActive.HasValue && x.IsActive.Value)
                    .ToList();
            }
        }

        [XmlIgnore]
        public List<Input> CheckInputs
        {
            get
            {
                return this.Inputs.Where(x => x.IsActive.HasValue && x.IsActive.Value && x.IsCheck.HasValue && x.IsCheck.Value)
                    .ToList();
            }
        }

        [XmlIgnore]
        public List<Input> ActiveInputs
        {
            get
            {
                return this.Inputs.Where(x => x.IsActive.HasValue && x.IsActive.Value && (!x.IsCheck.HasValue || !x.IsCheck.Value))
                    .ToList();
            }
        }

        [XmlIgnore]
        public List<Monitor> ActiveMonitors
        {
            get
            {
                return this.Monitors.Where(x => x.IsActive.HasValue && x.IsActive.Value)
                    .ToList();
            }
        }

        #endregion

        #endregion
    }
}
