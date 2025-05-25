using PacMan.ECS;
using PacMan.Game.Components;
using PacMan.Game.Services;

namespace PacMan.Game.Systems;

public class GhostMovementSystem(World world, IMazeService mazeService, IRandomNumberService randomNumberService) : IExecuteSystem
{
    static readonly Direction[] _directions = [Direction.Left, Direction.Right, Direction.Up, Direction.Down];
    
    public void Execute()
    {
        var ghosts = world.GetEntitiesWith<GhostComponent, PositionComponent>();
        foreach (var ghost in ghosts)
        {
            var pos = world.GetComponent<PositionComponent>(ghost);
            var direction = _directions[randomNumberService.RandomNumber(0, _directions.Length)];
            
            var newPos = direction switch
            {
                Direction.Left => pos with {X = pos.X - 1},
                Direction.Right => pos with {X = pos.X + 1},
                Direction.Up => pos with {Y = pos.Y - 1},
                Direction.Down => pos with {Y = pos.Y + 1},
                _ => pos
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
            world.ReplaceComponent(ghost, new PositionComponent(finalPosition.X, finalPosition.Y));
        }
    }
}
