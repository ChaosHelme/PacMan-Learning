using PacMan.ECS;

namespace PacMan.Game.Rendering;

public interface IRenderingProvider
{
    void Initialize(World world, Maze maze, IGameArtAssets assets);
    void Render();
}