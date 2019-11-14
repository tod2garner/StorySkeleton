using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StoryEngine
{
    public static class SerializeXML
    {
        public static void SaveToXML<T>(this T value, string FileName) where T : class
        {
            if (value == null)
                throw new ArgumentNullException("Null object cannot be serialized");

            using (var writer = new System.IO.StreamWriter(FileName))
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, value);
                writer.Flush();
            }
        }

        public static T LoadFromXML<T>(string FileName) where T : class
        {
            using (var stream = System.IO.File.OpenRead(FileName))
            {
                var serializer = new XmlSerializer(typeof(T));
                return serializer.Deserialize(stream) as T;
            }
        }

    }
}
