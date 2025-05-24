using PacMan.Ecs.Console.Components;

namespace PacMan.Ecs.Console.Systems;

public class MovementSystem
{
    private readonly World _world;
    private readonly Maze _maze;

    public MovementSystem(World world, Maze maze)
    {
        _world = world;
        _maze = maze;
    }

    public void MoveGhosts()
    {
        var ghosts = _world.GetEntitiesWith<GhostTag, PositionComponent>();
        var rand = new Random();
        foreach (var ghost in ghosts)
        {
            var pos = _world.GetComponent<PositionComponent>(ghost);
            var dirs = new[] { Direction.Left, Direction.Right, Direction.Up, Direction.Down }
                .OrderBy(_ => rand.Next()).ToList();
            foreach (var dir in dirs)
            {
                var newPos = dir switch
                {
                    Direction.Left => new PositionComponent(pos.X - 1, pos.Y),
                    Direction.Right => new PositionComponent(pos.X + 1, pos.Y),
                    Direction.Up => new PositionComponent(pos.X, pos.Y - 1),
                    Direction.Down => new PositionComponent(pos.X, pos.Y + 1),
                    _ => pos
                };
                if (_maze.IsWalkable(newPos.X, newPos.Y))
                {
                    _world.AddComponent(ghost, newPos);
                    break;
                }
            }
        }
    }
}
