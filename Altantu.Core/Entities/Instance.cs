namespace Altantu.Core.Entities
{
    /// <summary>
    /// Instance of an application deployed on an environment.
    /// </summary>
    public class Instance
    {
        #region Properties

        #region Xml properties

        public string Id { get; set; }

        public int SortIndex { get; set; }

        public bool? IsActive { get; set; }

        #endregion

        #endregion
    }
}
