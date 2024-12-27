namespace FileManager.Domain.Interfaces;

public interface IHashCalculator
{
    string CalculateHash(Stream fileStream);
}
