namespace PackMan.Console;

public class Maze
{
    public const int Height = 15;
    public const int Width = 20;
    public CellType[,] Cells { get; }
    public List<Position> Dots { get; }

    public Maze()
    {
        Cells = new CellType[Height, Width];
        Dots = new List<Position>();
        Initialize();
    }

    private void Initialize()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                if (IsWall(x, y))
                    Cells[y, x] = CellType.Wall;
                else
                {
                    Cells[y, x] = CellType.Dot;
                    Dots.Add(new Position(x, y));
                }
            }
        }
    }

    private static bool IsWall(int x, int y) =>
        x == 0 || x == Width - 1 || y == 0 || y == Height - 1 || (x % 2 == 0 && y % 2 == 0);

    public bool IsWalkable(Position pos) =>
        pos.X >= 0 && pos.X < Width && pos.Y >= 0 && pos.Y < Height && Cells[pos.Y, pos.X] != CellType.Wall;

    public bool HasDot(Position pos) => Dots.Contains(pos);

    public void RemoveDot(Position pos) => Dots.Remove(pos);

    public bool AllDotsCollected() => Dots.Count == 0;
}