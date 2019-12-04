namespace Altantu.Core.Interfaces
{
    public interface ITextFileService
    {
        #region Methods

        void WriteLine(string fileName, string message = null);

        #endregion
    }
}
