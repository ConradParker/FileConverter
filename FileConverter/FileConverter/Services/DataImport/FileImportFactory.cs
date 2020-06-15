using System;
using System.IO;

namespace FileConverter.Services.DataImport
{
    public class FileImportFactory
    {
        public static IFileImport FileImport(string filePath)
        {
            // Return the Import based on the file extension
            string fileExtension = Path.GetExtension(filePath);
            IFileImport fileImport = fileExtension switch
            {
                ".csv" => new CsvImport(filePath),
                _ => throw new NotSupportedException($"Unsupported file format '{fileExtension}'"),
            };
            return fileImport;
        }
    }
}
