using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Runtime.Serialization;

namespace StoryEngine
{
    public static class SerializeXML
    {
        public static void SaveToXML<T>(this T value, string FileName) where T : class
        {
            if (value == null)
                throw new ArgumentNullException("Null object cannot be serialized");

            var settings = new XmlWriterSettings() { Indent = true };
            using (var writer = XmlWriter.Create(FileName, settings))
            {
                var serializer = new DataContractSerializer(typeof(T));
                serializer.WriteObject(writer, value);
                writer.Flush();
            }
        }

        public static T LoadFromXML<T>(string FileName) where T : class
        {
            using (var stream = XmlReader.Create(FileName))
            {
                var serializer = new DataContractSerializer(typeof(T));
                return serializer.ReadObject(stream) as T;
            }
        }

    }
}
