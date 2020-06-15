using Newtonsoft.Json;

namespace FileConverter.Services.FileExport
{
    public class JsonExport : FileExport
    {
        public override string Export(dynamic data)
        {
            return JsonConvert.SerializeObject(data);
        }
    }
}
