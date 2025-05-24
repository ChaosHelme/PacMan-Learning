using PacMan.Game.Systems;

namespace PacMan.Game.Input;

public interface IInputProvider
{
    Direction GetDirection();
}
