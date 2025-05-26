using PacMan.ECS;
using PacMan.Game.Components;

namespace PacMan.Game.Systems;

public class PlayerDirectionSystem(World world) : IExecuteSystem
{
    public void Execute()
    {
        var inputEntity = world.GetEntitiesWith<InputComponent>().Single();
        var inputDirection = world.GetComponent<InputComponent>(inputEntity).Direction;
        var playerEntity = world.GetEntitiesWith<PlayerComponent>().Single();
        var playerDirection = world.GetComponent<DirectionComponent>(playerEntity).Direction;

        if (inputDirection != playerDirection && inputDirection != Direction.None)
        {
            world.ReplaceComponent(playerEntity, new DirectionComponent(inputDirection));
        }
    }
}