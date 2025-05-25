using PacMan.Game.Services;

namespace PacMan.Game.Configuration;

public static class MazeConfigurationLoader
{
    public static MazeConfiguration LoadMazeConfiguration(string filePath, IFileService fileService)
    {
        var lines = fileService.ReadAllLines(filePath);
        var config = new MazeConfiguration
        {
            Height = lines.Length,
            Width = lines.Length > 0 ? lines.Max(l => l.Length) : 0,
        };

        for (var y = 0; y < lines.Length; y++)
        {
            var line = lines[y];
            for (var x = 0; x < line.Length; x++)
            {
                switch (line[x])
                {
                    case '#':
                        config.WallCoordinates.Add((x, y));
                        break;
                    case '.':
                        config.DotCoordinates.Add((x, y));
                        break;
                }
            }
        }
        return config;
    }
}
