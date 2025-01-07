using System;
using System.IO;

namespace Advance_Of_C_.File_Operations
{
    public class BufferedStreamExample
    {
        private readonly string filePath;

        public BufferedStreamExample(string path)
        {
            filePath = path;
        }

        /// <summary>
        /// Check if the file exists.
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
        /// Append content to the file using BufferedStream.
        /// </summary>
        /// <param name="content">Content to append</param>
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
        /// Read content from the file using BufferedStream.
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
        /// Delete the file.
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
