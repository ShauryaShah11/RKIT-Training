using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Advance_Of_C_.Data_Serialization
{
    public class BinarySerialization
    {
        BinaryFormatter formatter = new BinaryFormatter();
        public void Serialize()
        {
            MyClass obj = new MyClass() { Id = 1, Name = "Shaurya" };
            using (FileStream fs = new FileStream("data.bin", FileMode.Create))
            {
                formatter.Serialize(fs, obj);
            }
        }

        public void DeSerialize()
        {
            using (FileStream fs = new FileStream("data.bin", FileMode.Open))
            {
                MyClass obj = (MyClass)formatter.Deserialize(fs);

                Console.WriteLine("Id :" + obj.Id);
                Console.WriteLine("Name :" + obj.Name);
            }
        }

    }
}
