using System.Collections.Generic;
using System.Linq;

namespace FileConverter.Services.DataImport
{
    public class CsvRow : List<Dictionary<string, object>>
    {
        public CsvRow(string line, List<string> headers)
        {
            var fieldIndex = headers.Count - 1;
            for (int i = 0; i < fieldIndex;)
            {
                foreach (var fieldValue in line.Split(','))
                {
                    ProcessField(headers[i++], fieldValue);
                }
            }
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

        private void ProcessField(string fieldName, object fieldValue)
        {
            // See if we have any sub properties
            var fieldObjectsArray = fieldName.Split('_');
            if (fieldObjectsArray.Length.Equals(1))
            {
                if (PropertyExists(fieldName, out Dictionary<string, object> existingObject))
                {
                    UpdateField(fieldName, (Dictionary<string, object>)fieldValue, existingObject);
                    return;
                }
            }
            else
            {
                var item = new Dictionary<string, object>
                {
                    { fieldObjectsArray.Last(), fieldValue }
                };
                ProcessField(fieldObjectsArray[^2], item);
                return;
            }

            Add(new CsvField(fieldName, GetFieldValue(fieldName, fieldValue)));
        }

        private bool PropertyExists(string fieldName, out Dictionary<string, object> existingObject)
        {
            // Check for an existing object
            existingObject = Find(x => x.ContainsKey(fieldName));

            if (existingObject != null)
            {
                return true;
            }

            return false;
        }

        private void UpdateField(string fieldName, Dictionary<string, object> fieldValue, Dictionary<string, object> existingObject)
        {
            // Get current values for this object
            var propertyValues = ((Dictionary<string, object>)existingObject[fieldName]);

            // Get incoming field value
            var dict = fieldValue.First();

            propertyValues.Add(dict.Key, dict.Value);

            // Update the existing object with the new item
            existingObject[fieldName] = propertyValues;
        }
    }
}
