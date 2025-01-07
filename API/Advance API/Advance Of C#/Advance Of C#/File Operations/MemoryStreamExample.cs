using System;
using System.IO;

namespace Advance_Of_C_.File_Operations
{
    public class MemoryStreamExample
    {
        private MemoryStream _memoryStream;

        public MemoryStreamExample()
        {
            // Initialize MemoryStream instance
            _memoryStream = new MemoryStream();
        }

        /// <summary>
        /// Method to write data to MemoryStream.
        /// </summary>
        public void WriteToMemoryStream(string content)
        {
            byte[] data = System.Text.Encoding.UTF8.GetBytes(content);
            _memoryStream.Write(data, 0, data.Length);
            Console.WriteLine("Data written to MemoryStream.");
        }

        /// <summary>
        /// Method to reset the position of the MemoryStream to the beginning.
        /// </summary>
        public void ResetMemoryStreamPosition()
        {
            // Reset position to 0 (beginning)
            _memoryStream.Position = 0;
            Console.WriteLine("MemoryStream position reset to the beginning.");
        }

        /// <summary>
        /// Method to read data from MemoryStream.
        /// </summary>
        public void ReadFromMemoryStream()
        {
            // Ensure we have data to read
            if (_memoryStream.Length > 0)
            {
                byte[] buffer = new byte[_memoryStream.Length];
                _memoryStream.Read(buffer, 0, buffer.Length);
                string content = System.Text.Encoding.UTF8.GetString(buffer);
                Console.WriteLine("Content from MemoryStream: " + content);
            }
            else
            {
                Console.WriteLine("MemoryStream is empty. No content to read.");
            }
        }

        /// <summary>
        /// Main method that writes and then reads from MemoryStream.
        /// </summary>
        public void PerformMemoryStreamOperations()
        {
            string contentToWrite = "Hello, this is a memory stream!";
            WriteToMemoryStream(contentToWrite); // Write data
            ResetMemoryStreamPosition();         // Reset position to start reading
            ReadFromMemoryStream();              // Read and display data
        }
    }
}
