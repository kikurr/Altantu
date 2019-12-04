using Altantu.Core.Extensions;
using System;
using System.ComponentModel;
using System.Threading;
using System.Xml.Serialization;

namespace Altantu.Core.Entities
{
    public class Monitor
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Values

        public enum StatusValues
        {
            Disabled,
            Unknown,
            Processing,
            Ok,
            Alert,
            Error
        }

        #endregion

        #region Fields

        private int? periodMiliseconds;
        private string intValueString;
        private int status;

        #endregion

        #region Methods

        public void Recalculate()
        {
            // Sample data to test.

            //if (this.IntValue.HasValue)
            //{
            //    if (this.Id == "ES Users")
            //    {
            //        this.IntValue = 128;
            //    }
            //    else if (this.Id == "FR Devices")
            //    {
            //        this.IntValue = 78124;
            //    }
            //    else if (this.Id == "FR Devices errors")
            //    {
            //        this.IntValue = 2139;
            //    }
            //}

            // Inactive.

            if (!this.IsActive.HasValue || !this.IsActive.Value)
            {
                this.Status = (int)StatusValues.Disabled;
                this.IntValueString = null;
            }

            // Error.

            else if (this.Status == (int)StatusValues.Error)
            {
                this.IntValue = null;
                this.IntValueString = "?";
            }

            // Unknown.

            else if (!this.IntValue.HasValue)
            {
                this.Status = (int)StatusValues.Unknown;
                this.IntValueString = null;
            }

            // Alert.

            else
            {
                bool isAlert = false;

                this.IntValueString = this.IntValue.Value.ToStringValue();

                if (this.ValueMin.HasValue && this.ValueMax.HasValue)
                {
                    isAlert = this.IntValue < this.ValueMin || this.IntValue > this.ValueMax;
                }
                else if (this.ValueMin.HasValue)
                {
                    isAlert = this.IntValue < this.ValueMin;
                }
                else if (this.ValueMax.HasValue)
                {
                    isAlert = this.IntValue > this.ValueMax;
                }

                this.Status = isAlert ? (int)StatusValues.Alert : (int)StatusValues.Ok;
            }
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Properties

        #region Xml properties

        public string FunctionId { get; set; }

        public string InputId { get; set; }

        public int? TimeOutSeconds { get; set; }

        public int? PeriodMinutes { get; set; }

        public int? ValueMin { get; set; }

        public int? ValueMax { get; set; }

        public bool? IsActive { get; set; }

        #endregion

        #region Xml subentity properties

        [XmlIgnore]
        public Function Function { get; set; }

        [XmlIgnore]
        public Input Input { get; set; }

        #endregion

        #region General properties

        [XmlIgnore]
        public string Id
        {
            get
            {
                return string.Format("{0} {1}", this.Function.Instance.Id, this.InputId);
            }
        }

        [XmlIgnore]
        public int PeriodMiliseconds
        {
            get
            {
                if (!this.periodMiliseconds.HasValue)
                {
                    if (this.PeriodMinutes.HasValue && this.PeriodMinutes.Value > 0)
                    {
                        this.periodMiliseconds = this.PeriodMinutes.Value * 60 * 1000;
                    }
                    else
                    {
                        this.periodMiliseconds = Timeout.Infinite;
                    }
                }
                return this.periodMiliseconds.Value;
            }
        }

        [XmlIgnore]
        public DateTime? Executed { get; set; }

        [XmlIgnore]
        public int? IntValue { get; set; }

        [XmlIgnore]
        public string IntValueString
        {
            get
            {
                return this.intValueString;
            }
            set
            {
                this.intValueString = value;
                this.NotifyPropertyChanged(nameof(Monitor.IntValueString));
            }
        }

        [XmlIgnore]
        public int Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;
                this.NotifyPropertyChanged(nameof(Monitor.Status));
            }
        }

        #endregion

        #endregion
    }
}
