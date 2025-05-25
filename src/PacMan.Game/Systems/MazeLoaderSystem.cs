using PacMan.ECS;
using PacMan.Game.Components;
using PacMan.Game.Configuration;

namespace PacMan.Game.Systems;

public class MazeLoaderSystem(World world, MazeConfiguration mazeConfiguration) : IInitializeSystem
{
    public void Initialize()
    {
        foreach (var wallCoordinate in mazeConfiguration.WallCoordinates)
        {
            var wall = world.CreateEntity();
            world.AddComponent(wall, new WallComponent());
            world.AddComponent(wall, new PositionComponent((wallCoordinate.X, wallCoordinate.Y)));
        }

        foreach (var dotCoordinate in mazeConfiguration.DotCoordinates)
        {
            var dot = world.CreateEntity();
            world.AddComponent(dot, new DotComponent());
            world.AddComponent(dot, new PositionComponent((dotCoordinate.X, dotCoordinate.Y)));
        }

        foreach (var warpCoordinate in mazeConfiguration.WarpCoordinates)
        {
            var warpPortal = world.CreateEntity();
            world.AddComponent(warpPortal, new WarpPortalComponent());
            world.AddComponent(warpPortal, new PositionComponent((warpCoordinate.X, warpCoordinate.Y)));
        }
    }
}