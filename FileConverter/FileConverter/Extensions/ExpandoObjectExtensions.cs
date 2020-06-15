using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace FileConverter.Extensions
{
    public static class ExpandoObjectExtensions
    {
        /// <summary>
        /// Convert Expando to Dictionary so we can more easily access it's properties
        /// </summary>        
        public static IDictionary<string, object> ToDictionary(this ExpandoObject expando)
        {
            return expando;
        }

        public static void AddProperty(this ExpandoObject expando, string propertyName, object propertyValue)
        {
            var expandoDictionary = expando.ToDictionary();
            if (expandoDictionary.ContainsKey(propertyName))
            {
                expando.UpdateProperty(propertyName, propertyValue);
            }
            else
            {
                expandoDictionary.Add(propertyName, propertyValue);
            }                
        }

        public static void UpdateProperty(this ExpandoObject expando, string propertyName, object propertyValue)
        {
            // Get current values for this object
            var expandoDictionary = expando.ToDictionary();
            var propertyValues = (Dictionary<string, object>)expandoDictionary[propertyName];

            // Get incoming field value
            var dict = ((Dictionary<string, object>)propertyValue).First();
            propertyValues.Add(dict.Key, dict.Value);

            // Update the existing object with the new item
            expandoDictionary[propertyName] = propertyValues;
        }

        public static bool TryGetPropertyValue(this ExpandoObject expando, string propertyName, out object propertyValue)
        {
            var expandoDictionary = expando as IDictionary<string, object>;
            return expandoDictionary.TryGetValue(propertyName, out propertyValue);
        }
    }
}
