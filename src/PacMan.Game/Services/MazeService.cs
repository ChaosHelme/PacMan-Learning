using PacMan.ECS;
using PacMan.Game.Components;
using PacMan.Game.Configuration;

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

    public bool IsWarpPortal(int x, int y)
    {
        foreach (var entity in world.GetEntitiesWith<WarpPortalComponent>())
        {
            var pos = world.GetComponent<PositionComponent>(entity);
            if (pos.X == x && pos.Y == y) return true;
        }

        return false;
    }
    
	public bool TryGetWarpDestination(int x, int y, out (int x, int y) destination)
	{
		// Find all warp entities in the same row
		var warpEntities = world.GetEntitiesWith<WarpPortalComponent>()
			.Select(world.GetComponent<PositionComponent>)
			.Where(pos => pos.Y == y)
			.ToList();
		
		destination = (x, y);
		if (warpEntities.Count < 2)
			return false; // No warp or only one warp in this row

		// Find the other warp position in the same row
		destination = warpEntities
			.Select(s => (s.X, s.Y))
			.FirstOrDefault(pos => pos.X != x);
		
		return destination != default;
	}
}