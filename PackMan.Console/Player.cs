namespace PackMan.Console;

public class Player : IActor
{
    public Position Position { get; set; }

    public Player(Position start)
    {
        Position = start;
    }

    public void Move(Direction direction, Maze maze)
    {
        var newPos = Position.Move(direction);
        if (maze.IsWalkable(newPos))
            Position = newPos;
    }
}