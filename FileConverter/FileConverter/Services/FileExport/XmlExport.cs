using Newtonsoft.Json;
using System.IO;
using System.Xml;

namespace FileConverter.Services.FileExport
{
    public class XmlExport : FileExport
    {
        public override string Export(dynamic data)
        {
            var json = JsonConvert.SerializeObject(data);

            // convert JSON to XML Document
            var xmlDoc = JsonConvert.DeserializeXmlNode("{\"row\":" + json + "}", "root");

            using var stringWriter = new StringWriter();
            using var xmlTextWriter = XmlWriter.Create(stringWriter);
            xmlDoc.WriteTo(xmlTextWriter);
            xmlTextWriter.Flush();

            return stringWriter.GetStringBuilder().ToString();
        }
    }
}
