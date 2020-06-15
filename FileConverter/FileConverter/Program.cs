using FileConverter.Enums;
using FileConverter.Services.DataImport;
using FileConverter.Services.FileExport;
using System;
using System.IO;

namespace FileConverter
{
    class Program
    {
        static void Main()
        {
            // Display title on console.
            Console.WriteLine("File converter in C#\r");
            Console.WriteLine("------------------------\n");

            // Ask user for input file to convert.
            Console.WriteLine("Put in the path to your file");
            var filePath = Console.ReadLine();

            // Validate
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File {filePath} does not exist!");
            }

            // Process the import file
            var processedData = FileImportFactory
                .FileImport(filePath)
                .GetData();

            // Ask the user to choose the output format.
            Console.WriteLine("Choose an output format from the following list:");
            Console.WriteLine("\tj - json");
            Console.WriteLine("\tx - xml");
            Console.Write("Your option? ");

            var format = (Console.ReadLine()) switch
            {
                "j" => Formats.JSON,
                "x" => Formats.XML,
                _ => throw new ArgumentException($"Unknown format"),
            };

            var fileExport = FileExportFactory.FileExport(format);
            var result = fileExport.Export(processedData);

            var newFileName = $"{Path.GetDirectoryName(filePath)}\\" +
                $"{Path.GetFileNameWithoutExtension(filePath)}.{Enum.GetName(format.GetType(), format).ToLower()}";
            File.Create(newFileName).Dispose();
            File.AppendAllText(newFileName, result);

            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}
