namespace FileConverter.Services.FileExport
{
    public abstract class FileExport : IFileExport
    {
        public abstract string Export(dynamic data);
    }
}
