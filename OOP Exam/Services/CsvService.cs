using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Exam.Services
{
    public class CsvService : ICsvSerdeService
    {
        public IEnumerable<T> Deserialize<T>(string path, char delimiter) where T : new()
        {
            string[] csvHeaders = File.ReadLines(path).First().Split(delimiter);
            PropertyInfo?[] properties = GetPropertiesFromCsvHeader<T>(csvHeaders);

            return File.ReadLines(path)
                .Skip(1)
                .Select(csvRow => DeserializeSingleCsvRow<T>(csvRow.Split(delimiter), properties))
                .ToList();
        }

        public void Serialize<T>(string path, char delimiter, IEnumerable<T> value)
        {
            throw new NotImplementedException();
        }

        private PropertyInfo?[] GetPropertiesFromCsvHeader<T>(string[] csvHeaders)
        {
            Type type = typeof(T);
            List<PropertyInfo?> commonProperties = new();

            foreach (string header in csvHeaders)
            {
                string propertyName = FirstLetterToUpper(header);
                PropertyInfo? property = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
                commonProperties.Add(property);
            }
            return commonProperties.ToArray();
        }

        private static T DeserializeSingleCsvRow<T>(string[] csvValues, PropertyInfo?[] properties) where T : new()
        {
            T result = new();

            foreach (var (property, csvValue) in properties.Zip(csvValues))
            {
                if (property == null || !property.CanWrite || csvValue == null)
                {
                    continue;
                }

                var thisPropertyValue = Convert.ChangeType(csvValue, property.PropertyType);
                property.SetValue(result, thisPropertyValue, null);

            }

            return result;
        }
        private static string FirstLetterToUpper(string input)
        {
            return char.ToUpper(input[0]) + input[1..];
        }
    }
}
