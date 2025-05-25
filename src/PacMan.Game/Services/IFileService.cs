namespace PacMan.Game.Services;

public interface IFileService
{
    /// <summary>
    /// Reads all lines from the given filePath
    /// </summary>
    /// <remarks>
    /// Returns an empty string array if the file cannot be found or cannot be read
    /// </remarks>
    /// <param name="filePath">The absolut path to the maze configuration file</param>
    /// <returns>A string array containing all lines read or an empty string array if any error occured.</returns>
    string[] ReadAllLines(string filePath);
}