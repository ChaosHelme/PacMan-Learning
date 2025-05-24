namespace PacMan.Game.Services;

public class RandomNumberService : IRandomNumberService
{
    public int RandomNumber(int min, int max) => Random.Shared.Next(min, max);

    public float RandomSingle() => Random.Shared.NextSingle();
}