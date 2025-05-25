using PacMan.ECS;
using PacMan.Game.Configuration;
using PacMan.Game.Services;

namespace PacMan.Game.Rendering;

public interface IRenderingProvider
{
    void Initialize(World world, MazeConfiguration mazeConfiguration, IMazeService mazeService, IGameArtAssets assets);
    void Render();
}