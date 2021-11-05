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
        public IEnumerable<T> Deserialize<T>(string path, char delimiter)
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
            return RemoveHtmlTags(source).Trim('"');
        }

        private static string RemoveHtmlTags(string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }
    }
}
//public static T Deserialize<T>(string csvString, char csvSeparator) where T : class
//{
//    Type typeToBeReturned = typeof(T);
//    object[] stringValues = csvString.Split(csvSeparator);
//    ConstructorInfo constructorToBeCalled = typeToBeReturned.GetConstructors()[0];
//    ParameterInfo[] constructorParameters = constructorToBeCalled.GetParameters();

//    ConstructorInfo info = typeof(User).GetConstructors()[0];
//    ParameterInfo paraminfo = info.GetParameters()[0];

//    // typeof(User).GetConstructor(new Type[] { typeof(string), typeof(int) }).Invoke(new object[] { "as", 5 });

//    // User newuser = new(id: 5, firstname: "Da", lastname: "da", username: "da", balance: 50, email: "da@email.com");


//    object[] parametersToBePassedToConstructor = new object[constructorParameters.Length];

//    for (int i = 0; i < constructorParameters.Length; i++)
//    {
//        var converter = TypeDescriptor.GetConverter(constructorParameters[i].ParameterType);
//        parametersToBePassedToConstructor[i] = converter.ConvertFrom(stringValues[i]);
//    }


//    T result = (T)constructorToBeCalled.Invoke(parametersToBePassedToConstructor);
//    //T result = (T)Activator.CreateInstance(typeToBeReturned, parametersToBePassedToConstructor);
//    return result;
//}