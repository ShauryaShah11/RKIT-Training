using System;
using System.IO;

/// <summary>
/// The FileUtility class demonstrates basic file operations in C#.
/// It includes various methods such as CreateFile, ReadFile, AppendToFile, DeleteFile, CheckIfFileExists, and CopyFile.
/// </summary>
class FileUtility
{
    /// <summary>
    /// The CreateFile method creates a new file with the specified name and writes text to it.
    /// </summary>
    /// <param name="filePath">The file path location where the file will be created.</param>
    public void CreateFile(string filePath)
    {
        try
        {
            Console.WriteLine("Enter text to write in file: ");
            string text = Console.ReadLine();
            File.WriteAllText(filePath, text);
            Console.WriteLine("File created successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating file: {ex.Message}");
        }
    }

    /// <summary>
    /// The ReadFile method reads the content of the file at the specified path.
    /// </summary>
    /// <param name="filePath">The file path location to read the file content from.</param>
    public void ReadFile(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                string text = File.ReadAllText(filePath);
                Console.WriteLine("File content:");
                Console.WriteLine(text);
            }
            else
            {
                Console.WriteLine("File not found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
        }
    }

    /// <summary>
    /// The AppendToFile method appends text to the file at the specified path.
    /// </summary>
    /// <param name="filePath">The file path location to append data to the file.</param>
    public void AppendToFile(string filePath)
    {
        try
        {
            Console.WriteLine("Enter text to append to file: ");
            string text = Console.ReadLine();
            File.AppendAllText(filePath, text);
            Console.WriteLine("Text appended successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error appending to file: {ex.Message}");
        }
    }

    /// <summary>
    /// The DeleteFile method deletes the file at the specified path.
    /// </summary>
    /// <param name="filePath">The file path location of the file to be deleted.</param>
    public void DeleteFile(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Console.WriteLine("File deleted successfully.");
            }
            else
            {
                Console.WriteLine("File not found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting file: {ex.Message}");
        }
    }

    /// <summary>
    /// The CheckIfFileExists method checks if a file exists at the specified path.
    /// </summary>
    /// <param name="filePath">The file path location to check for the file.</param>
    /// <returns>True if the file exists, otherwise false.</returns>
    public bool CheckIfFileExists(string filePath)
    {
        return File.Exists(filePath);
    }

    /// <summary>
    /// The CopyFile method copies a file from the source path to the destination path.
    /// </summary>
    /// <param name="sourcePath">The source file path location from which data will be copied.</param>
    /// <param name="destinationPath">The destination file path location where data will be written.</param>
    /// <param name="overwrite">Indicates whether to overwrite the destination file if it already exists.</param>
    public void CopyFile(string sourcePath, string destinationPath, bool overwrite = false)
    {
        try
        {
            File.Copy(sourcePath, destinationPath, overwrite);
            Console.WriteLine("File copied successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error copying file: {ex.Message}");
        }
    }
}


