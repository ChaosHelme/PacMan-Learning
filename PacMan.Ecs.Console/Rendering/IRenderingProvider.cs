namespace PacMan.Ecs.Console.Rendering;

public interface IRenderingProvider
{
    void Initialize(World world, Maze maze);
    void Render();
}