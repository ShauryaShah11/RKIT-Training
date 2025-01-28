using System;
using System.IO;

namespace Advance_Of_C_.File_Operations
{
    /// <summary>
    /// Demonstrates various file operations using FileStream.
    /// This includes checking file existence, appending content, updating file, 
    /// checking file permissions, working with directories, displaying file information, 
    /// and deleting the file.
    /// </summary>
    public class FileStreamExample
    {
        private readonly string filePath;

        /// <summary>
        /// Initializes the FileStreamExample with a specified file path.
        /// </summary>
        /// <param name="path">The file path to be used for the operations.</param>
        public FileStreamExample(string path)
        {
            filePath = path;
        }

        /// <summary>
        /// Checks if the file exists at the specified file path.
        /// </summary>
        public void CheckFileExistence()
        {
            try
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
            catch (IOException ex)
            {
                Console.WriteLine("File I/O error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
        }

        /// <summary>
        /// Appends content to the file using FileStream in append mode.
        /// </summary>
        /// <param name="content">The content to append to the file.</param>
        public void AppendToFile(string content)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write))
                {
                    byte[] data = System.Text.Encoding.UTF8.GetBytes(content);
                    fs.Write(data, 0, data.Length);
                    Console.WriteLine("Data appended to the file.");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("File I/O error: " + ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Permission error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
        }

        /// <summary>
        /// Updates (overwrites) the content of the file with new content.
        /// </summary>
        /// <param name="newContent">The new content to write to the file.</param>
        public void UpdateFile(string newContent)
        {
            try
            {
                File.WriteAllText(filePath, newContent);
                Console.WriteLine("File content updated.");
            }
            catch (IOException ex)
            {
                Console.WriteLine("File I/O error: " + ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Permission error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
        }

        /// <summary>
        /// Checks the permissions of the file (read-only or writable).
        /// </summary>
        public void CheckFilePermissions()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("File does not exist.");
                    return;
                }

                FileAttributes attributes = File.GetAttributes(filePath);
                if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    Console.WriteLine("The file is read-only.");
                }
                else
                {
                    Console.WriteLine("The file is writable.");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("File I/O error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
        }

        /// <summary>
        /// Checks if the path corresponds to a directory.
        /// </summary>
        /// <returns>True if the path is a directory; otherwise, false.</returns>
        public bool IsDirectory()
        {
            try
            {
                if (!Directory.Exists(filePath))
                {
                    Console.WriteLine("Directory doesn't exist");
                    return false;
                }

                FileAttributes attributes = File.GetAttributes(filePath);
                if ((attributes & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    return true;
                }
                return false;
            }
            catch (IOException ex)
            {
                Console.WriteLine("File I/O error: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Displays detailed file information such as size, creation time, and modification time using FileInfo.
        /// </summary>
        public void DisplayFileInfo()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("File does not exist.");
                    return;
                }

                FileInfo fileInfo = new FileInfo(filePath);

                Console.WriteLine("File Information:");
                Console.WriteLine($"Full Name: {fileInfo.FullName}");
                Console.WriteLine($"Directory: {fileInfo.DirectoryName}");
                Console.WriteLine($"Size: {fileInfo.Length} bytes");
                Console.WriteLine($"Created: {fileInfo.CreationTime}");
                Console.WriteLine($"Last Accessed: {fileInfo.LastAccessTime}");
                Console.WriteLine($"Last Modified: {fileInfo.LastWriteTime}");
                Console.WriteLine($"Is Read-Only: {fileInfo.IsReadOnly}");
            }
            catch (IOException ex)
            {
                Console.WriteLine("File I/O error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
        }

        /// <summary>
        /// Deletes the file at the specified file path if it exists.
        /// </summary>
        public void DeleteFile()
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
                    Console.WriteLine("File does not exist, cannot delete.");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("File I/O error: " + ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Permission error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
        }
    }
}
