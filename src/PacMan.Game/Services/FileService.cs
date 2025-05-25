namespace PacMan.Game.Services;

public class FileService : IFileService
{
    public string[] ReadAllLines(string filePath) => File.ReadAllLines(filePath);
}