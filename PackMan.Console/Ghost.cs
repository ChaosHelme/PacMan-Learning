namespace PackMan.Console;

public class Ghost : IActor
{
    public Position Position { get; set; }
    private static readonly Random _random = new();

    public Ghost(Position start)
    {
        Position = start;
    }

    public void Move(Direction direction, Maze maze)
    {
        var newPos = Position.Move(direction);
        if (maze.IsWalkable(newPos))
            Position = newPos;
    }

    // Random movement for simplicity
    public void MoveRandom(Maze maze)
    {
        var directions = new[] { Direction.Left, Direction.Right, Direction.Up, Direction.Down }
            .OrderBy(_ => _random.Next()).ToList();

        foreach (var dir in directions)
        {
            var newPos = Position.Move(dir);
            if (maze.IsWalkable(newPos))
            {
                Position = newPos;
                break;
            }
        }
    }
}