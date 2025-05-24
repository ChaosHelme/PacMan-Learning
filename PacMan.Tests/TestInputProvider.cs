using PacMan.Game.Input;
using PacMan.Game.Systems;

namespace PacMan.Tests;

public class TestInputProvider(IEnumerable<Direction> inputs) : IInputProvider
{
    private readonly Queue<Direction> _inputs = new(inputs);

    public Direction GetDirection() => _inputs.Count > 0 ? _inputs.Dequeue() : Direction.None;

    public void AddInput(Direction direction) => _inputs.Enqueue(direction);
}
