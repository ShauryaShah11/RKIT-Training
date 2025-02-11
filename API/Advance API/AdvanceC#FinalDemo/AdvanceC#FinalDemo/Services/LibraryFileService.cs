using AdvanceC_FinalDemo.Models;
using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;

namespace AdvanceC_FinalDemo.Services
{
    /// <summary>
    /// The LibraryFileService class contains method performing serialization and deserizalization operation with file.
    /// </summary>
    public class LibraryFileService
    {
        private readonly string _baseDirectory;

        public LibraryFileService()
        {
            _baseDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
            string dataFolderPath = Path.Combine(_baseDirectory, "data");
            Directory.CreateDirectory(_baseDirectory);
        }

        /// <summary>
        /// Serializes DataTable to a JSON file.
        /// </summary>
        public Response SerializeDataTable(DataTable dataTable, string fileName)
        {
            try
            {
                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    return new Response
                    {
                        IsError = true,
                        Message = "Invalid DataTable for serialization"
                    };
                }

                string filePath = Path.Combine(_baseDirectory, fileName);

                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    string jsonString = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
                    sw.Write(jsonString);
                }

                return new Response
                {
                    IsError = false,
                    Message = $"DataTable serialized successfully to {filePath}"
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsError = true,
                    Message = $"Error during DataTable serialization: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Deserializes DataTable from a JSON file.
        /// </summary>
        public Response DeserializeDataTable(string fileName)
        {
            try
            {
                string filePath = Path.Combine(_baseDirectory, fileName);

                if (!File.Exists(filePath))
                {
                    return new Response
                    {
                        IsError = true,
                        Message = $"File not found: {filePath}"
                    };
                }

                using (StreamReader sr = new StreamReader(filePath))
                {
                    string jsonString = sr.ReadToEnd();
                    DataTable dataTable = JsonConvert.DeserializeObject<DataTable>(jsonString);

                    return new Response
                    {
                        IsError = false,
                        Data = dataTable,
                        Message = $"DataTable deserialized successfully from {filePath}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsError = true,
                    Message = $"Error during DataTable deserialization: {ex.Message}"
                };
            }
        }
    }
}
