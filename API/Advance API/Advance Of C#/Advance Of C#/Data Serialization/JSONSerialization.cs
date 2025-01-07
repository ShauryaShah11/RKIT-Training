using System;
using System.IO;
using Newtonsoft.Json;

namespace Advance_Of_C_.Data_Serialization
{
    public class JSONSerialization
    {
        private readonly string filePath = "data.json";

        /// <summary>
        /// Serializes an object to a JSON file.
        /// </summary>
        public void Serialize()
        {
            // Create an object to serialize
            MyClass obj = new MyClass { Id = 1, Name = "Shaurya" };

            try
            {
                // Serialize the object to JSON and write to file
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    string jsonString = JsonConvert.SerializeObject(obj, Formatting.Indented); // Indented for readability
                    sw.Write(jsonString);
                    Console.WriteLine("Serialized JSON:\n" + jsonString);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during serialization: " + ex.Message);
            }
        }

        /// <summary>
        /// Deserializes a JSON file back into an object.
        /// </summary>
        public void Deserialize()
        {
            try
            {
                // Ensure the file exists before attempting to read
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("File not found: " + filePath);
                    return;
                }

                // Read the JSON file and deserialize it into an object
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string jsonString = sr.ReadToEnd();
                    MyClass obj = JsonConvert.DeserializeObject<MyClass>(jsonString);

                    // Print the object's properties
                    Console.WriteLine("Deserialized Object:");
                    Console.WriteLine($"Id: " + obj.Id);
                    Console.WriteLine($"Name: " + obj.Name);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during deserialization: " + ex.Message);
            }
        }
    }
}
    

