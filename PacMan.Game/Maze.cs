namespace PacMan.Game;

public class Maze
{
    public const int Height = 20, Width = 25;
    private readonly bool[,] _walls = new bool[Height, Width];
    private readonly bool[,] _dots = new bool[Height, Width];

    public Maze()
    {
        for (var y = 0; y < Height; y++)
            for (var x = 0; x < Width; x++)
            {
                if (IsWall(x, y))
                    _walls[y, x] = true;
                else
                    _dots[y, x] = true;
            }
    }

    private static bool IsWall(int x, int y)
    {
        if (x == 0 || x == Width - 1 || y == 0)
            return true;

        return y == Height - 1 || (x % 2 == 0 && y % 2 == 0);
    }

    public bool IsWalkable(int x, int y)
    {
        if (x is < 0 or >= Width)
            return false;

        return y is >= 0 and < Height && !_walls[y, x];
    }

    public void RemoveDot(int x, int y) => _dots[y, x] = false;

    public bool HasDot(int x, int y) => _dots[y, x];
    public bool IsWallAt(int x, int y) => _walls[y, x];
}
