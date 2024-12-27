using File = System.IO.File;

namespace FileManager.Infrastructure.Services;

public class FileStorageService : IFileStorageService
{
    private readonly string _storagePath;

    public FileStorageService(string storagePath)
    {
        _storagePath = storagePath;
    }

    public string SaveFile(string fileName, Stream fileStream)
    {
        var filePath = Path.Combine(_storagePath, fileName);

        var directoryPath = Path.GetDirectoryName(filePath);

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        using (var fileStreamOutput = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        {
            fileStream.CopyTo(fileStreamOutput);
        }

        return filePath;
    }

    public void DeleteFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}