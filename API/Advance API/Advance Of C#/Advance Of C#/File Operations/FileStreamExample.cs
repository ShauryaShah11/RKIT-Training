using System;
using System.IO;

namespace Advance_Of_C_.File_Operations
{
    public class FileStreamExample
    {
        private readonly string filePath;

        public FileStreamExample(string path)
        {
            filePath = path;
        }

        // Opens the file in ReadWrite mode
        public void Open()
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
                {
                    Console.WriteLine("File opened successfully.");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error opening file: " + ex.Message);
            }
        }

        // Reads the content of the file
        public void Read()
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (StreamReader reader = new StreamReader(fs))
                {
                    Console.WriteLine("File content:\n" + reader.ReadToEnd());
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error reading file: " + ex.Message);
            }
        }

        // Writes new content to the file (overwrites existing content)
        public void Write(string content)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(content);
                    Console.WriteLine("Content written to file.");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error writing file: " + ex.Message);
            }
        }

        // Appends new content to the file
        public void Append(string content)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.Read))
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.WriteLine(content);
                    Console.WriteLine("Content appended to file.");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error appending to file: " + ex.Message);
            }
        }

        // Deletes the file
        public void Delete()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    Console.WriteLine("File deleted.");
                }
                else
                {
                    Console.WriteLine("File does not exist.");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error deleting file: " + ex.Message);
            }
        }
    }    
}
