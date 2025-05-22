using System.Threading.Channels;

namespace PackMan.Console;

public class InputHandler
{
    private readonly Channel<ConsoleKey> _inputChannel = Channel.CreateUnbounded<ConsoleKey>();

    public InputHandler()
    {
        _ = Task.Run(ReadInputAsync);
    }

    private async Task ReadInputAsync()
    {
        while (true)
        {
            var key = System.Console.ReadKey(true).Key;
            await _inputChannel.Writer.WriteAsync(key);
        }
    }

    public Direction GetDirection()
    {
        if (_inputChannel.Reader.TryRead(out var key))
        {
            return key switch
            {
                ConsoleKey.LeftArrow => Direction.Left,
                ConsoleKey.RightArrow => Direction.Right,
                ConsoleKey.UpArrow => Direction.Up,
                ConsoleKey.DownArrow => Direction.Down,
                ConsoleKey.Q => Direction.Quit,
                _ => Direction.None
            };
        }
        return Direction.None;
    }
}