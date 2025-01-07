using System;
using System.IO;

namespace Advance_Of_C_.Base_Class_Library
{
    class FileIOExample
    {
        public void DemonstrateFileIO()
        {
            string path = "example.txt";

            // Writing to a file
            File.WriteAllText(path, "Hello, this is a test file!");
            Console.WriteLine("File written successfully!");

            // Reading from a file
            if (File.Exists(path))
            {
                string content = File.ReadAllText(path);
                Console.WriteLine("File Content: " + content);
            }
        }
    }
}
