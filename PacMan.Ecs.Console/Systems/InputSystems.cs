namespace PacMan.Ecs.Console.Systems;

public class InputSystem
{
    public Direction LastDirection { get; private set; } = Direction.None;

    public void Poll()
    {
        if (!System.Console.KeyAvailable) return;
        var key = System.Console.ReadKey(true).Key;
        LastDirection = key switch
        {
            ConsoleKey.LeftArrow => Direction.Left,
            ConsoleKey.RightArrow => Direction.Right,
            ConsoleKey.UpArrow => Direction.Up,
            ConsoleKey.DownArrow => Direction.Down,
            ConsoleKey.Q => Direction.Quit,
            _ => Direction.None
        };
    }
}
public enum Direction { None, Left, Right, Up, Down, Quit }
