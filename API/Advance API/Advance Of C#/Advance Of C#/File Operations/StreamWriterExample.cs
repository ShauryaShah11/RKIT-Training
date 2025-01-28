using System;
using System.IO;

namespace Advance_Of_C_.File_Operations
{
    /// <summary>
    /// This class demonstrates how to use the StreamWriter class in C# for writing text data to a file.
    /// It includes various methods to write text (single-line, multi-line, with encoding) and append data to a file.
    /// StreamWriter handles character-based output and allows for efficient writing to files.
    /// </summary>
    public class StreamWriterExample
    {
        private readonly string filePath;

        public StreamWriterExample(string path)
        {
            filePath = path;
        }

        /// <summary>
        /// Method to write a single string of text to the file.
        /// </summary>
        public void WriteTextToFile(string content)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.Write(content);
                    Console.WriteLine("Data written to file.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while writing text to the file: {ex.Message}");
            }
        }

        /// <summary>
        /// Method to read and display the content of a file using StreamReader.
        /// </summary>
        public void ReadFile()
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string fileContents = reader.ReadToEnd(); // Store the contents of the file
                    Console.WriteLine(fileContents);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
            }
        }

        /// <summary>
        /// Method to write multiple lines of text to the file, one line at a time.
        /// </summary>
        public void WriteLinesToFile(string[] lines)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while writing lines to the file: {ex.Message}");
            }
        }

        /// <summary>
        /// Method to write text to a file with a specific encoding (UTF-8 in this case).
        /// </summary>
        public void WriteTextWithEncoding(string content)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
                {
                    writer.Write(content);
                    Console.WriteLine("Data written with UTF-8 encoding.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while writing text with encoding: {ex.Message}");
            }
        }

        /// <summary>
        /// Method to append text to an existing file.
        /// </summary>
        public void AppendTextToFile(string content)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.Write(content);
                    Console.WriteLine("Data appended to file.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while appending text to the file: {ex.Message}");
            }
        }
    }
}
