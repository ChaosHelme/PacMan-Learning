using System.Diagnostics.CodeAnalysis;

namespace PacMan.Game.Services;

// This class is a simple wrapper around the .Net File.ReadAllLines method.
// The purpose, why we have this wrapper is to be able to use a different implementation for unit testing.
[ExcludeFromCodeCoverage]
public class FileService : IFileService
{
    public string[] ReadAllLines(string filePath) => File.ReadAllLines(filePath);
}