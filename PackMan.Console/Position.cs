namespace PackMan.Console;

public record struct Position(int X, int Y)
{
    public Position Move(Direction direction) => direction switch
    {
        Direction.Left => new Position(X - 1, Y),
        Direction.Right => new Position(X + 1, Y),
        Direction.Up => new Position(X, Y - 1),
        Direction.Down => new Position(X, Y + 1),
        _ => this
    };
}