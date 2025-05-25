using PacMan.ECS;
using PacMan.Game.Components;

namespace PacMan.Game.Services;

public class MazeService(World world) : IMazeService
{
    public bool IsWallAt(int x, int y)
    {
        foreach (var entity in world.GetEntitiesWith<WallComponent>())
        {
            var pos = world.GetComponent<PositionComponent>(entity);
            if (pos.X == x && pos.Y == y) return true;
        }
        return false;
    }

    public bool IsDotAt(int x, int y)
    {
        foreach (var entity in world.GetEntitiesWith<DotComponent>())
        {
            var pos = world.GetComponent<PositionComponent>(entity);
            if (pos.X == x && pos.Y == y) return true;
        }
        return false;
    }

    public bool IsWalkable(int x, int y) => !IsWallAt(x, y);
}