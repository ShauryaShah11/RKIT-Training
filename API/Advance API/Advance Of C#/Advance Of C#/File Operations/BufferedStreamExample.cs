using System;
using System.IO;

namespace Advance_Of_C_.File_Operations
{
    /// <summary>
    /// This class demonstrates various file operations using BufferedStream.
    /// It includes checking file existence, appending content to a file,
    /// reading content from a file, and deleting the file.
    /// BufferedStream is used to enhance performance when reading and writing 
    /// to files by minimizing the number of I/O operations.
    /// </summary>
    public class BufferedStreamExample
    {
        private readonly string filePath;

        /// <summary>
        /// Initializes a new instance of the BufferedStreamExample class with a specified file path.
        /// </summary>
        /// <param name="path">The file path for the file operations.</param>
        public BufferedStreamExample(string path)
        {
            filePath = path;
        }

        /// <summary>
        /// Checks if the file exists at the specified path.
        /// </summary>
        public void CheckFileExistence()
        {
            if (File.Exists(filePath))
            {
                Console.WriteLine("File already exists.");
            }
            else
            {
                Console.WriteLine("File does not exist.");
            }
        }

        /// <summary>
        /// Appends content to the file using BufferedStream for improved performance.
        /// </summary>
        /// <param name="content">The content to append to the file.</param>
        public void AppendToFile(string content)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write))
            using (BufferedStream bs = new BufferedStream(fs))
            {
                byte[] data = System.Text.Encoding.UTF8.GetBytes(content);
                bs.Write(data, 0, data.Length);
                Console.WriteLine("Data appended to the file using BufferedStream.");
            }
        }

        /// <summary>
        /// Reads the content from the file using BufferedStream for improved performance.
        /// </summary>
        public void ReadFromFile()
        {
            if (File.Exists(filePath))
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                using (BufferedStream bs = new BufferedStream(fs))
                {
                    byte[] buffer = new byte[fs.Length];
                    bs.Read(buffer, 0, buffer.Length);
                    string content = System.Text.Encoding.UTF8.GetString(buffer);
                    Console.WriteLine("File Content: " + content);
                }
            }
            else
            {
                Console.WriteLine("File does not exist.");
            }
        }

        /// <summary>
        /// Deletes the file at the specified path if it exists.
        /// </summary>
        public void DeleteFile()
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Console.WriteLine("File deleted.");
            }
            else
            {
                Console.WriteLine("File does not exist, cannot delete.");
            }
        }
    }
}
