namespace PacMan.Game.Configuration;

public class MazeConfiguration
{
    public int Width { get; init; }
    public int Height { get; init; }
    public List<(int X, int Y)> WallCoordinates { get; } = new();
    public List<(int X, int Y)> DotCoordinates { get; } = new();
}
