using System.Xml.Serialization;

namespace Altantu.Core.Entities
{
    public class Function
    {
        #region Values

        public enum FunctionTypes
        {
            Database,
            WebAppPool
        }

        #endregion

        #region Properties

        #region Xml properties

        /// <summary>
        /// Name of datababase or WebAppPool depending on FunctionType.
        /// </summary>
        public string Id { get; set; }

        public string Server { get; set; }

        public string InstanceId { get; set; }

        public string FunctionType { get; set; }

        public bool? IsActive { get; set; }

        #endregion

        #region Xml subentity properties

        [XmlIgnore]
        public Instance Instance { get; set; }

        #endregion

        #endregion
    }
}
