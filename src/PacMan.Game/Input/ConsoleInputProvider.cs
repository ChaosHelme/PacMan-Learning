using System.Diagnostics.CodeAnalysis;
using PacMan.Game.Systems;

namespace PacMan.Game.Input;

// This class has direct dependency on System.Console and relies on user input.
// Therefor we won't test this implementation of IInputProvider.
// The interface itself is used to have a test implementation for integration testing.
[ExcludeFromCodeCoverage]
public class ConsoleInputProvider : IInputProvider
{
    public Direction GetDirection()
    {
        if (!System.Console.KeyAvailable)
            return Direction.None;

        var key = System.Console.ReadKey(true).Key;
        return InputMapper.MapKey(key);
    }
}
