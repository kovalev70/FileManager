namespace FileManager.Infrastructure.Services;

public class HashCalculator : IHashCalculator
{
    public string CalculateHash(Stream fileStream)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashBytes = sha256.ComputeHash(fileStream);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
        }
    }
}