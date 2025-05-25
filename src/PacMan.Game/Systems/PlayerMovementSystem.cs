using PacMan.ECS;
using PacMan.Game.Components;
using PacMan.Game.Services;

namespace PacMan.Game.Systems;

public class PlayerMovementSystem(World world, IMazeService mazeService) : IExecuteSystem
{
    public void Execute()
    {
        var playerEntity = world.GetEntitiesWith<PlayerComponent>().Single();
        var currentPlayerDirection = world.GetComponent<DirectionComponent>(playerEntity).Direction;
        var pos = world.GetComponent<PositionComponent>(playerEntity);
        
        if (currentPlayerDirection is Direction.None or Direction.Quit) return;
        
        var newPos = currentPlayerDirection switch
        {
            Direction.Left => pos with {X = pos.X - 1},
            Direction.Right => pos with {X = pos.X + 1},
            Direction.Up => pos with {Y = pos.Y - 1},
            Direction.Down => pos with {Y = pos.Y + 1},
            _ => pos,
        };
        
        var isWarpPortal = mazeService.IsWarpPortal(newPos.X, newPos.Y);
        var isWalkable = mazeService.IsWalkable(newPos.X, newPos.Y);
        var finalPosition = (pos.X, pos.Y);
        if (isWalkable && !isWarpPortal)
        {
            finalPosition = (newPos.X, newPos.Y);
        } else if (isWalkable && isWarpPortal)
        {
            finalPosition = mazeService.GetWarpDestination((newPos.X, newPos.Y));
        }
        world.ReplaceComponent(playerEntity, new PositionComponent(finalPosition.X, finalPosition.Y));
    }
}