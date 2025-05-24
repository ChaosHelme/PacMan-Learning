namespace PacMan.Game.Services;

public interface IRandomNumberService
{
    int RandomNumber(int min, int max);
    float RandomSingle();
}