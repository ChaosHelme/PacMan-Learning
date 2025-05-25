using PacMan.Game.Services;

namespace PacMan.Tests;

public class TestFileService(string fileContent) : IFileService
{
    public string[] ReadAllLines(string filePath) => string.IsNullOrWhiteSpace(fileContent) ? [] : fileContent.Split('\n');
}