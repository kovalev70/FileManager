namespace FileManager.Domain.Interfaces;

public interface IFileStorageService
{
    string SaveFile(string fileName, Stream fileStream);
    void DeleteFile(string filePath);
}
