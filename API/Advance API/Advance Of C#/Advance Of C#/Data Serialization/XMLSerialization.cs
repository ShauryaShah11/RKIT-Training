using System;
using System.IO;
using System.Xml.Serialization;

namespace Advance_Of_C_.Data_Serialization
{
    public class XMLSerialization
    {
        XmlSerializer formatter = new XmlSerializer(typeof(MyClass));
        public void Serialize()
        {
            MyClass obj = new MyClass() { Id = 1, Name = "Shaurya" };
            using(StreamWriter sw = new StreamWriter("data.xml"))
            {
                formatter.Serialize(sw, obj);
            }
            Console.WriteLine("Object serialized to XML successfully.");
        }

        public void Deserialize()
        {
            using(StreamReader sr = new StreamReader("data.xml"))
            {
                MyClass obj = (MyClass)formatter.Deserialize(sr);

                Console.WriteLine("Id : "+obj.Id);
                Console.WriteLine("Name : "+obj.Name);
            }
        }
    }
}
