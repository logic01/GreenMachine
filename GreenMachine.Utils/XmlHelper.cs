using System.IO;
using System.Xml.Serialization;

namespace GreenMachine.Utils
{
    public static class XmlHelper
    {
        public static string SerializeToXmlString(object obj)
        {
            var serializer = new XmlSerializer(obj.GetType());

            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, obj);

                return writer.ToString();
            }
        }
    }
}