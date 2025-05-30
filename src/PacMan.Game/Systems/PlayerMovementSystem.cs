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
        var pos = world.GetComponent<PositionComponent>(playerEntity).Position;
        
        if (currentPlayerDirection is Direction.None or Direction.Quit) return;
        
        var newPos = currentPlayerDirection switch
        {
            Direction.Left => pos with {X = pos.X - 1},
            Direction.Right => pos with {X = pos.X + 1},
            Direction.Up => pos with {Y = pos.Y - 1},
            Direction.Down => pos with {Y = pos.Y + 1},
            _ => pos,
        };
        
        var isWalkable = mazeService.IsWalkable(newPos.X, newPos.Y);
		mazeService.TryGetWarpDestination(newPos.X, newPos.Y, out var finalPosition);
        if (isWalkable)
        {
			world.ReplaceComponent(playerEntity, new PositionComponent((finalPosition.x, finalPosition.y)));
        }
    }
}