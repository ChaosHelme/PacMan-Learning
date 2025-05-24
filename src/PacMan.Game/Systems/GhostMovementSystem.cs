using PacMan.ECS;
using PacMan.Game.Components;

namespace PacMan.Game.Systems;

public class GhostMovementSystem(World world, Maze maze) : IExecuteSystem
{
    public void Execute()
    {
        var ghosts = world.GetEntitiesWith<GhostComponent, PositionComponent>();
        var rand = new Random();
        foreach (var ghost in ghosts)
        {
            var pos = world.GetComponent<PositionComponent>(ghost);
            var dirs = new[] { Direction.Left, Direction.Right, Direction.Up, Direction.Down }
                .OrderBy(_ => rand.Next()).ToList();
            foreach (var dir in dirs)
            {
                var newPos = dir switch
                {
                    Direction.Left => pos with {X = pos.X - 1},
                    Direction.Right => pos with {X = pos.X + 1},
                    Direction.Up => pos with {Y = pos.Y - 1},
                    Direction.Down => pos with {Y = pos.Y + 1},
                    _ => pos
                };
                if (maze.IsWalkable(newPos.X, newPos.Y))
                {
                    world.ReplaceComponent(ghost, newPos);
                    break;
                }
            }
        }
    }
}
