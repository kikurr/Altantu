using System.Collections.Generic;

namespace Altantu.Core.Entities
{
    public class Input
    {
        #region Properties

        #region Xml properties

        public string Id { get; set; }

        public bool? IsCheck { get; set; }

        public int SortIndex { get; set; }

        public string FunctionType { get; set; }

        public List<string> Parameters { get; set; }

        public int? TimeOutSeconds { get; set; }

        public int? PeriodMinutes { get; set; }

        public int? ValueMin { get; set; }

        public int? ValueMax { get; set; }

        public bool? IsActive { get; set; }

        #endregion

        #endregion
    }
}
