using PacMan.Ecs.Console.Input;
using PacMan.Ecs.Console.Systems;

namespace PacMan.Tests;

public class TestInputProvider : IInputProvider
{
    private readonly Queue<Direction> _inputs;

    public TestInputProvider(IEnumerable<Direction> inputs)
    {
        _inputs = new Queue<Direction>(inputs);
    }

    public Direction GetDirection()
    {
        return _inputs.Count > 0 ? _inputs.Dequeue() : Direction.None;
    }
}
