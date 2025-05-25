using PacMan.ECS;
using PacMan.Game.Components;
using PacMan.Game.Configuration;

namespace PacMan.Game.Services;

public class MazeService(World world, MazeConfiguration mazeConfiguration) : IMazeService
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

    public bool IsWarpPortal(int x, int y)
    {
        foreach (var entity in world.GetEntitiesWith<WarpPortalComponent>())
        {
            var pos = world.GetComponent<PositionComponent>(entity);
            if (pos.X == x && pos.Y == y) return true;
        }

        return false;
    }
    
    public (int X, int Y) GetWarpDestination((int x, int y) source)
    {
        if (mazeConfiguration.WarpCoordinates.Contains(source))
        {
            // Find the other warp point in the same row (but different column)
            var other = mazeConfiguration.WarpCoordinates
                .FirstOrDefault(coord => coord.Y == source.y && coord.X != source.x);

            // If found, return it; otherwise, stay in place
            if (other != default)
                return other;
        }
        return source;
    }
}