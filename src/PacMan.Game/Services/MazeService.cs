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
            var positionComponent = world.GetComponent<PositionComponent>(entity);
            if (positionComponent.Position.X == x && positionComponent.Position.Y == y) return true;
        }
        return false;
    }

    public bool IsDotAt(int x, int y)
    {
        foreach (var entity in world.GetEntitiesWith<DotComponent>())
        {
            var positionComponent = world.GetComponent<PositionComponent>(entity);
            if (positionComponent.Position.X == x && positionComponent.Position.Y == y) return true;
        }
        return false;
    }

    public bool IsWalkable(int x, int y) => !IsWallAt(x, y);
	
	public bool TryGetWarpDestination(int x, int y, out (int x, int y) destination)
	{
		// Find all warp entities in the same row
		var warpEntities = world.GetEntitiesWith<WarpPortalComponent>()
			.Select(world.GetComponent<PositionComponent>)
			.Where(component => component.Position.Y == y)
			.ToList();
		
		destination = (x, y);
		if (warpEntities.Count < 2)
			return false; // No warp or only one warp in this row

		// Find the other warp position in the same row
		destination = warpEntities
			.Select(component => (component.Position.X, component.Position.Y))
			.FirstOrDefault(pos => pos.X != x);
		
		return destination != default;
	}
}