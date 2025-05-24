using PacMan.Game.Components;
using PacMan.Game.Ecs;

namespace PacMan.Game.Systems;

public class PlayerDirectionSystem(World world) : IExecuteSystem
{
    public void Execute()
    {
        var inputEntity = world.GetUniqueComponentOwner<InputComponent>();
        var inputDirection = world.GetComponent<InputComponent>(inputEntity).Direction;
        var playerEntity = world.GetUniqueComponentOwner<PlayerComponent>();
        var playerDirection = world.GetComponent<DirectionComponent>(playerEntity).Direction;

        if (inputDirection != playerDirection && inputDirection != Direction.None)
        {
            world.ReplaceComponent(playerEntity, new DirectionComponent(inputDirection));
        }
    }
}