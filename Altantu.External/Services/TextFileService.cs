using Altantu.Core.Interfaces;
using System.IO;

namespace Altantu.External.Services
{
    public class TextFileService : ITextFileService
    {
        #region Methods

        public void WriteLine(string fileName, string message = null)
        {
            try
            {
                StreamWriter writer = new StreamWriter(new FileStream(fileName, FileMode.Append, FileAccess.Write));
                using (writer)
                {
                    writer.WriteLine(message);
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
