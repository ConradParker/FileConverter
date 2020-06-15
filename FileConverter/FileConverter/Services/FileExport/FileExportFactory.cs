using FileConverter.Enums;
using System;

namespace FileConverter.Services.FileExport
{
    public class FileExportFactory
    {
        public static IFileExport FileExport(Formats format)
        {
            IFileExport fileExport;
            fileExport = (format) switch
            {
                Formats.JSON => new JsonExport(),
                Formats.XML => new XmlExport(),
                _ => throw new NotSupportedException($"Unsupported file format '{format}'"),
            };
            return fileExport;
        }
    }
}
