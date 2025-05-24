using PacMan.Game.Components;
using PacMan.Game.Ecs;

namespace PacMan.Game.Systems;

public class PlayerMovementSystem(World world, Maze maze) : IExecuteSystem
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
        
        if (maze.IsWalkable(newPos.X, newPos.Y))
        {
            world.ReplaceComponent(playerEntity, newPos);
        }
    }
}