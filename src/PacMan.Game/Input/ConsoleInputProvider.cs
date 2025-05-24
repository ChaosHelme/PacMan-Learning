using PacMan.Game.Systems;

namespace PacMan.Game.Input;

public class ConsoleInputProvider : IInputProvider
{
    public Direction GetDirection()
    {
        if (!System.Console.KeyAvailable)
            return Direction.None;

        var key = System.Console.ReadKey(true).Key;
        return key switch
        {
            ConsoleKey.LeftArrow => Direction.Left,
            ConsoleKey.RightArrow => Direction.Right,
            ConsoleKey.UpArrow => Direction.Up,
            ConsoleKey.DownArrow => Direction.Down,
            ConsoleKey.A => Direction.Left,
            ConsoleKey.D => Direction.Right,
            ConsoleKey.W => Direction.Up,
            ConsoleKey.S => Direction.Down,
            ConsoleKey.Q => Direction.Quit,
            _ => Direction.None
        };
    }
}
