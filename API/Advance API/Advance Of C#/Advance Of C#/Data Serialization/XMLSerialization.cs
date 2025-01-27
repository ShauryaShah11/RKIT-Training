using System;
using System.IO;
using System.Xml.Serialization;

namespace Advance_Of_C_.Data_Serialization
{
    /// <summary>
    /// This class demonstrates XML serialization and deserialization using the XmlSerializer.
    /// Serialization converts an object into an XML format, which can be stored or transmitted, 
    /// and deserialization converts an XML file back into an object.
    /// </summary>
    public class XMLSerialization
    {
        XmlSerializer formatter = new XmlSerializer(typeof(MyClass));

        /// <summary>
        /// Serializes an object into an XML file.
        /// The object is converted to XML and written to the "data.xml" file.
        /// </summary>
        public void Serialize()
        {
            MyClass obj = new MyClass() { Id = 1, Name = "Shaurya" };
            using (StreamWriter sw = new StreamWriter("data.xml"))
            {
                formatter.Serialize(sw, obj);
            }
            Console.WriteLine("Object serialized to XML successfully.");
        }

        /// <summary>
        /// Deserializes an XML file back into an object.
        /// Reads the XML file, converts it back to an object, and prints its properties.
        /// </summary>
        public void Deserialize()
        {
            using (StreamReader sr = new StreamReader("data.xml"))
            {
                MyClass obj = (MyClass)formatter.Deserialize(sr);

                Console.WriteLine("Id : " + obj.Id);
                Console.WriteLine("Name : " + obj.Name);
            }
        }
    }
}
