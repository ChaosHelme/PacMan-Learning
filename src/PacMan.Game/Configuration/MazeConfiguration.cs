namespace PacMan.Game.Configuration;

public class MazeConfiguration
{
    public int Width { get; init; }
    public int Height { get; init; }
    public List<(int X, int Y)> WallCoordinates { get; } = [];
    public List<(int X, int Y)> DotCoordinates { get; } = [];
    public List<(int X, int Y)> WarpCoordinates { get; } = [];
}
