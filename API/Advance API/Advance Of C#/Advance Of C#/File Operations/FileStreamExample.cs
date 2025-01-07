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
        /// Append content to the file.
        /// </summary>
        /// <param name="content">Content to append</param>
        public void AppendToFile(string content)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write))
            {
                byte[] data = System.Text.Encoding.UTF8.GetBytes(content);
                fs.Write(data, 0, data.Length);
                Console.WriteLine("Data appended to the file.");
            }
        }

        /// <summary>
        /// Update content of the file.
        /// </summary>
        /// <param name="newContent">New content to overwrite</param>
        public void UpdateFile(string newContent)
        {
            File.WriteAllText(filePath, newContent);
            Console.WriteLine("File content updated.");
        }

        /// <summary>
        /// Check file permissions (read-only or writable).
        /// </summary>
        public void CheckFilePermissions()
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

        /// <summary>
        /// Check For Directory.
        /// </summary>
        /// <returns>True if path is Directory else false.</returns>
        public bool IsDirectory()
        {
            if (!Directory.Exists(filePath))
            {
                Console.WriteLine("Directory doesnt exist");
                return false; ;
            }
            FileAttributes attributes = File.GetAttributes(filePath);
            if((attributes & FileAttributes.Directory) == FileAttributes.Directory)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Display file information using FileInfo.
        /// </summary>
        public void DisplayFileInfo()
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
