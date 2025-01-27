using System;
using System.IO;
using Newtonsoft.Json;

namespace Advance_Of_C_.Data_Serialization
{
    /// <summary>
    /// This class demonstrates JSON serialization and deserialization using the Newtonsoft.Json library.
    /// Serialization converts an object into a JSON string, which can be stored or transmitted, 
    /// and deserialization converts a JSON string back into an object.
    /// </summary>
    public class JSONSerialization
    {
        private readonly string filePath = "data.json";

        /// <summary>
        /// Serializes an object into a JSON file.
        /// The object is converted to a JSON string and written to the "data.json" file.
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
        /// Reads the JSON file, converts it back to an object, and prints its properties.
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
