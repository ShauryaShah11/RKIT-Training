using System;
using System.IO;

namespace Advance_Of_C_.File_Operations
{
    /// <summary>
    /// This class demonstrates how to use the MemoryStream class in C# for reading and writing data to memory.
    /// It includes methods to write, read, and reset the position of the MemoryStream. 
    /// MemoryStream allows for efficient in-memory data manipulation without writing to disk.
    /// </summary>
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
        /// Converts the input string into a byte array and writes it to the MemoryStream.
        /// </summary>
        public void WriteToMemoryStream(string content)
        {
            try
            {
                byte[] data = System.Text.Encoding.UTF8.GetBytes(content);
                _memoryStream.Write(data, 0, data.Length);
                Console.WriteLine("Data written to MemoryStream.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while writing to MemoryStream: {ex.Message}");
            }
        }

        /// <summary>
        /// Method to reset the position of the MemoryStream to the beginning.
        /// Allows reading from the start of the MemoryStream after writing data.
        /// </summary>
        public void ResetMemoryStreamPosition()
        {
            try
            {
                // Reset position to 0 (beginning)
                _memoryStream.Position = 0;
                Console.WriteLine("MemoryStream position reset to the beginning.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while resetting MemoryStream position: {ex.Message}");
            }
        }

        /// <summary>
        /// Method to read data from MemoryStream.
        /// Reads the content from the current position in the MemoryStream and displays it.
        /// </summary>
        public void ReadFromMemoryStream()
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading from MemoryStream: {ex.Message}");
            }
        }

        /// <summary>
        /// Main method that writes and then reads from MemoryStream.
        /// Performs the operations of writing, resetting position, and reading the data.
        /// </summary>
        public void PerformMemoryStreamOperations()
        {
            try
            {
                string contentToWrite = "Hello, this is a memory stream!";
                WriteToMemoryStream(contentToWrite); // Write data
                ResetMemoryStreamPosition();         // Reset position to start reading
                ReadFromMemoryStream();              // Read and display data
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while performing memory stream operations: {ex.Message}");
            }
        }
    }
}
