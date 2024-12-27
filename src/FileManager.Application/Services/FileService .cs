using File = FileManager.Domain.Entities.File;

namespace FileManager.Application.Services;

public class FileService : IFileService
{
    private readonly IRepository<File> _fileRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IFileStorageService _fileStorageService;
    private readonly IHashCalculator _hashCalculator;

    public FileService(
        IRepository<File> fileRepository,
        IRepository<User> userRepository,
        IFileStorageService fileStorageService,
        IHashCalculator hashCalculator)
    {
        _fileRepository = fileRepository;
        _userRepository = userRepository;
        _fileStorageService = fileStorageService;
        _hashCalculator = hashCalculator;
    }

    public async Task<IEnumerable<FileDto>> GetUserFilesAsync(string userName)
    {
        var user = (await _userRepository.GetAllAsync()).FirstOrDefault(u => u.UserName == userName);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found");
        }

        var files = (await _fileRepository.GetAllAsync()).Where(f => f.UserId == user.Id).ToList();
        return files.Select(f => new FileDto
        {
            Id = f.Id,
            FileName = f.Name,
            FilePath = f.Path,
            Hash = f.Hash,
            UploadedAt = f.UploadedAt
        });
    }

    public async Task<FileDto> UploadFileAsync(string userName, string fileName, Stream fileStream)
    {
        var user = (await _userRepository.GetAllAsync()).FirstOrDefault(u => u.UserName == userName);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found");
        }

        var fileMemoryStream = new MemoryStream();
        await fileStream.CopyToAsync(fileMemoryStream);
        var fileBytes = fileMemoryStream.ToArray();
        var fileHash = _hashCalculator.CalculateHash(new MemoryStream(fileBytes));
        var existingFile = (await _fileRepository.GetAllAsync()).FirstOrDefault(f => f.Hash == fileHash);
        if (existingFile != null)
        {
            throw new FileAlreadyExistsException("File with the same hash already exists");
        }

        var filePath = _fileStorageService.SaveFile(fileName, new MemoryStream(fileBytes));

        var file = new File
        {
            Name = fileName,
            Path = filePath,
            Hash = fileHash,
            UploadedAt = DateTime.UtcNow,
            UserId = user.Id
        };

        await _fileRepository.AddAsync(file);

        return new FileDto
        {
            Id = file.Id,
            FileName = file.Name,
            FilePath = file.Path,
            Hash = file.Hash,
            UploadedAt = file.UploadedAt
        };
    }

    public async Task DeleteFileAsync(int fileId)
    {
        var file = await _fileRepository.GetByIdAsync(fileId);
        if (file == null)
        {
            throw new KeyNotFoundException("File not found");
        }

        _fileStorageService.DeleteFile(file.Path);

        await _fileRepository.DeleteAsync(file.Id);
    }

    public async Task<FileDto> GetFileByIdAsync(int fileId)
    {
        var file = await _fileRepository.GetByIdAsync(fileId);
        if (file == null)
        {
            throw new KeyNotFoundException("File not found");
        }

        return new FileDto
        {
            Id = file.Id,
            FileName = file.Name,
            FilePath = file.Path,
            Hash = file.Hash,
            UploadedAt = file.UploadedAt
        };
    }
}