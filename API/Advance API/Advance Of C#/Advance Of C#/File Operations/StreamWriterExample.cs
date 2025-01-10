using System;
using System.IO;

namespace Advance_Of_C_.File_Operations
{
    public class StreamWriterExample
    {
        private readonly string filePath;

        public StreamWriterExample(string path)
        {
            filePath = path;
        }

        // Write text to a file
        public void WriteTextToFile(string content)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.Write(content);
                Console.WriteLine("Data written to file.");
            }
        }

        public void ReadFile()
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string fileContents = reader.ReadToEnd(); // Store the contents of the file
                Console.WriteLine(fileContents);
            }
        }

        // Write multiple lines to a file
        public void WriteLinesToFile(string[] lines)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var line in lines)
                {
                    writer.WriteLine(line);
                }
                Console.WriteLine("Lines written to file.");
            }
        }

        // Write data with a specific encoding (e.g., UTF-8)
        public void WriteTextWithEncoding(string content)
        {
            using (StreamWriter writer = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
            {
                writer.Write(content);
                Console.WriteLine("Data written with UTF-8 encoding.");
            }
        }

        // Append text to a file
        public void AppendTextToFile(string content)
        {
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.Write(content);
                Console.WriteLine("Data appended to file.");
            }
        }
    }
}
