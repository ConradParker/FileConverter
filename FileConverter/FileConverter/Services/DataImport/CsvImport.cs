using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using FileConverter.Extensions;

namespace FileConverter.Services.DataImport
{
    public class CsvImport : IFileImport
    {
        List<string> headers;
        readonly List<string> lines;

        public CsvImport(string filePath)
        {
            lines = new List<string>();

            File.ReadLines(filePath)
                .ToList()
                .ForEach(row =>
                {
                    if (headers == null)
                    {
                        headers = row.Split(',').ToList();
                    }
                    else
                    {
                        lines.Add(row);
                    }
                });
        }

        private object GetFieldValue(string fieldName, object fieldValue)
        {
            var fieldObjectsArray = fieldName.Split('_');
            if (fieldObjectsArray.Length.Equals(1))
            {
                return fieldValue;
            }
            else
            {
                var item = new Dictionary<string, object>
                {
                    { fieldObjectsArray.Last(), fieldValue }
                };
                return GetFieldValue(fieldObjectsArray[^2], item);
            }
        }

        private void ProcessField(ExpandoObject rowData, string fieldName, object fieldValue)
        {
            // See if we have any sub properties
            var fieldObjectsArray = fieldName.Split('_');
            if (fieldObjectsArray.Length > 1)
            {
                var item = new Dictionary<string, object>
                {
                    { fieldObjectsArray.Last(), fieldValue }
                };
                ProcessField(rowData, fieldObjectsArray[^2], item);
                return;
            }

            rowData.AddProperty(fieldName, GetFieldValue(fieldName, fieldValue));
        }

        public List<dynamic> GetData()
        {
            var data = new List<dynamic>();
            var fieldIndex = headers.Count - 1;

            foreach (var line in lines)
            {
                dynamic rowData = new ExpandoObject();

                for (int i = 0; i < fieldIndex;)
                {
                    foreach (var fieldValue in line.Split(','))
                    {
                        ProcessField(rowData, headers[i++], fieldValue);
                    }
                }

                data.Add(rowData);
            }

            return data;
        }
    }
}
