using OOP_Exam.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OOP_Exam.Services
{
    public class CsvSerde : ICsvSerdeService
    {
        public IEnumerable<T> Deserialize<T>(string path, char delimiter) where T : new()
        {
            ConstructorInfo constructorToBeCalled = typeof(T).GetConstructors()[0];
            ParameterInfo[] constructorParameters = constructorToBeCalled.GetParameters();
            List<TypeConverter> typeConverters = new();
            List<T> deserializedObjects = new();

            foreach (ParameterInfo parameter in constructorParameters)
            {
                typeConverters.Add(TypeDescriptor.GetConverter(parameter.ParameterType));
            }

            try
            {
                deserializedObjects = File.ReadAllLines(path)
                    .Skip(1)
                    .Select(csvRow => DeserializeSingleCsvRow<T>(csvRow.Split(delimiter),
                                                               constructorToBeCalled,
                                                               typeConverters))
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Use logging service eventually?
            }

            return deserializedObjects;
        }

        public void Serialize<T>(string path, char delimiter, IEnumerable<T> value)
        {
            throw new NotImplementedException();
        }

        private static T DeserializeSingleCsvRow<T>(string[] stringValues, ConstructorInfo ctor, List<TypeConverter> parameterTypes)
        {
            object[] parametersToBePassedToConstructor = new object[parameterTypes.Count];

            for (int i = 0; i < parameterTypes.Count; i++)
            {
                string cleanString = CleanString(stringValues[i]);
                parametersToBePassedToConstructor[i] = parameterTypes[i].ConvertFrom(cleanString);
            }
            return (T)ctor.Invoke(parametersToBePassedToConstructor);
        }

        private static string CleanString(string source)
        {
            return RemoveHtmlTags(source).Trim(new[] { '"', ' ' });
        }

        private static string RemoveHtmlTags(string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }
    }
}
