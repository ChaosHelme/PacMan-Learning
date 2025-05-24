using PacMan.ECS;
using PacMan.Game.Components;
using PacMan.Game.Input;

namespace PacMan.Game.Systems;

public class InputSystem(World world, IInputProvider inputProvider) : IExecuteSystem
{
    public void Execute()
    {
        var inputEntity = world.GetUniqueComponentOwner<InputComponent>();
        world.ReplaceComponent(inputEntity, new InputComponent(inputProvider.GetDirection()));
    }
}


public enum Direction { None, Left, Right, Up, Down, Quit }
