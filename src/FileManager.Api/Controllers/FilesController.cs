[ApiController]
[Route("api/[controller]")]
public class FilesController : ControllerBase
{
    private readonly IGetUserFilesUseCase _getUserFilesUseCase;
    private readonly IUploadFileUseCase _uploadFileUseCase;
    private readonly IDownloadFileUseCase _downloadFileUseCase;
    private readonly IDeleteFileUseCase _deleteFileUseCase;

    public FilesController(
        IGetUserFilesUseCase getUserFilesUseCase,
        IUploadFileUseCase uploadFileUseCase,
        IDownloadFileUseCase downloadFileUseCase,
        IDeleteFileUseCase deleteFileUseCase)
    {
        _getUserFilesUseCase = getUserFilesUseCase;
        _uploadFileUseCase = uploadFileUseCase;
        _downloadFileUseCase = downloadFileUseCase;
        _deleteFileUseCase = deleteFileUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetUserFiles()
    {
        var userName = User.Identity.Name;
        if (string.IsNullOrEmpty(userName))
        {
            return Unauthorized("User is not authenticated.");
        }

        var files = await _getUserFilesUseCase.ExecuteAsync(userName);
        return Ok(files);
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile([FromForm] UploadFileRequest file)
    {
        var userName = User.Identity.Name;
        if (string.IsNullOrEmpty(userName))
        {
            return Unauthorized("User is not authenticated.");
        }

        if (file.File == null || file.File.Length == 0)
        {
            return BadRequest("Invalid file.");
        }

        using (var stream = file.File.OpenReadStream())
        {
            try
            {
                var fileDto = await _uploadFileUseCase.ExecuteAsync(userName, file.File.FileName, stream);
                return Ok(fileDto);
            }
            catch (FileAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
        }
    }

    [HttpGet("download/{fileId}")]
    public async Task<IActionResult> DownloadFile(int fileId)
    {
        var userName = User.Identity.Name;
        if (string.IsNullOrEmpty(userName))
        {
            return Unauthorized("User is not authenticated.");
        }

        var response = await _downloadFileUseCase.ExecuteAsync(fileId);

        return Ok(response);
    }

    [HttpDelete("{fileId}")]
    public async Task<IActionResult> DeleteFile(int fileId)
    {
        var userName = User.Identity.Name;
        if (string.IsNullOrEmpty(userName))
        {
            return Unauthorized("User is not authenticated.");
        }

        try
        {
            await _deleteFileUseCase.ExecuteAsync(fileId);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
