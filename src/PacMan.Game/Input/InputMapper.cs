using PacMan.Game.Systems;

namespace PacMan.Game.Input;

public class InputMapper
{
	public static Direction MapKey(ConsoleKey key) => key switch
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