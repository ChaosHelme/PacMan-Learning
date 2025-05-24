using PacMan.Ecs.Console.Components;

namespace PacMan.Ecs.Console.Systems;

public class PlayerMovementSystem(World world, Maze maze) : IExecuteSystem
{
    public void Execute()
    {
        var playerEntity = world.GetEntitiesWith<PlayerTag>().Single();
        var dir = world.GetComponent<DirectionComponent>(playerEntity).Value;
        var pos = world.GetComponent<PositionComponent>(playerEntity);
        
        if (dir == Direction.None || dir == Direction.Quit) return;
        
        var newPos = dir switch
        {
            Direction.Left => pos with {X = pos.X - 1},
            Direction.Right => pos with {X = pos.X + 1},
            Direction.Up => pos with {Y = pos.Y - 1},
            Direction.Down => pos with {Y = pos.Y + 1},
            _ => pos,
        };
        
        if (maze.IsWalkable(newPos.X, newPos.Y))
        {
            world.AddComponent(playerEntity, newPos);
        }
    }
}