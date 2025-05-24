namespace PacMan.Game.Core;

public interface IGameRunner
{
    Task Run(int frameDelay);
}