using System.Collections.Generic;

namespace FileConverter.Services.DataImport
{
    public abstract class FileImport : IFileImport
    {
        public string FilePath { get; set; }

        public FileImport(string filePath)
        {
            FilePath = filePath;
        }

        public abstract List<dynamic> GetData();
    }
}
