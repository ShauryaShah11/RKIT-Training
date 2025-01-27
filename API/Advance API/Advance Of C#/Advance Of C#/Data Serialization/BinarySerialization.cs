using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Advance_Of_C_.Data_Serialization
{
    /// <summary>
    /// This class demonstrates binary serialization and deserialization in C#.
    /// It uses the BinaryFormatter to serialize an object to a file and deserialize it back to an object.
    /// Serialization converts an object into a byte stream for storage or transmission, and deserialization converts it back.
    /// </summary>
    public class BinarySerialization
    {
        BinaryFormatter formatter = new BinaryFormatter();

        /// <summary>
        /// Serializes an instance of MyClass and stores it in a binary file "data.bin".
        /// </summary>
        public void Serialize()
        {
            MyClass obj = new MyClass() { Id = 1, Name = "Shaurya" };
            using (FileStream fs = new FileStream("data.bin", FileMode.Create))
            {
                formatter.Serialize(fs, obj); // Convert object to a byte stream and save it to a file
            }
        }

        /// <summary>
        /// Deserializes the binary file "data.bin" back into an object of MyClass.
        /// It reads the data from the file and outputs the object's properties.
        /// </summary>
        public void DeSerialize()
        {
            using (FileStream fs = new FileStream("data.bin", FileMode.Open))
            {
                MyClass obj = (MyClass)formatter.Deserialize(fs); // Convert byte stream back to object

                Console.WriteLine("Id :" + obj.Id);
                Console.WriteLine("Name :" + obj.Name);
            }
        }

    }
}
