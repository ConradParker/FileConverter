using System.Collections.Generic;

namespace FileConverter.Services.DataImport
{
    public class CsvField : Dictionary<string, object>
    {
        public CsvField(string name, object value)
        {
            Add(name, value);
        }
    }
}
