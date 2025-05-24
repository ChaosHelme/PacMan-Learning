using PacMan.Game.Services;

namespace PacMan.Tests.Services;

public class TestRandomNumberService : IRandomNumberService
{
    private Queue<int> _inputs = new();
    public void PreloadRandomNumbers(IEnumerable<int> inputs) => _inputs = new(inputs);
    public int RandomNumber(int min, int max) =>_inputs.Dequeue();

    public float RandomSingle() => 0f;
}