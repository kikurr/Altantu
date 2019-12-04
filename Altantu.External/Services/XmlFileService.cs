using Altantu.Core.Interfaces;
using System.Xml;
using System.Xml.Serialization;

namespace Altantu.External.Services
{
    public class XmlFileService : IXmlFileService
    {
        #region Methods

        public T GetItem<T>(string fileName)
        {
            try
            {
                T item = default(T);

                XmlSerializer serializer = new XmlSerializer(typeof(T));
                XmlReader reader = XmlReader.Create(fileName);
                using (reader)
                {
                    item = (T)serializer.Deserialize(reader);
                }

                return item;
            }
            catch
            {
                throw;
            }
        }

        public void SaveItem<T>(string fileName, T item)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                XmlWriter writer = XmlWriter.Create(fileName);
                using (writer)
                {
                    serializer.Serialize(writer, item);
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
