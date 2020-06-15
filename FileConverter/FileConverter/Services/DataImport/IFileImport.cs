using System.Collections.Generic;

namespace FileConverter.Services.DataImport
{
    public interface IFileImport
    {
        List<dynamic> GetData();
    }
}
