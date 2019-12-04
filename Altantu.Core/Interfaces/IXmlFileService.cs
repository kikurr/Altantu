namespace Altantu.Core.Interfaces
{
    public interface IXmlFileService
    {
        #region Methods

        T GetItem<T>(string fileName);

        void SaveItem<T>(string fileName, T item);

        #endregion
    }
}
