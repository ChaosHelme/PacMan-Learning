using PacMan.Ecs.Console.Systems;

namespace PacMan.Ecs.Console.Input;

public interface IInputProvider
{
    Direction GetDirection();
}
