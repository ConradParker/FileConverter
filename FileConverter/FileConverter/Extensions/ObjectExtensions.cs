using System.IO;
using System.Xml.Serialization;

namespace FileConverter.Extensions
{
    public static class ObjectExtensions
    {
        public static string SerializeObjectToXml<T>(this T toSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
        }
    }
}
