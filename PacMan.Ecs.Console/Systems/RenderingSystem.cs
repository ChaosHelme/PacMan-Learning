using PacMan.Ecs.Console.Components;
using PacMan.Ecs.Console.Rendering;
using Spectre.Console;

namespace PacMan.Ecs.Console.Systems;

public class RenderingSystem
{
    private readonly World _world;
    private readonly Maze _maze;
    readonly IRenderingProvider _renderingProvider;

    

    public RenderingSystem(World world, Maze maze, IRenderingProvider renderingProvider)
    {
        _world = world;
        _maze = maze;
        _renderingProvider = renderingProvider;
    }

    public void Render()
    {
        _renderingProvider.Render();
        
        
    }

    public void ShowGameOver(int score)
    {
        AnsiConsole.Cursor.SetPosition(0, Maze.Height + 2);
        AnsiConsole.Write(
            new Panel($"[bold]Game Over![/]\nFinal Score: {score}")
                .Border(BoxBorder.Rounded)
                .Header("[yellow]PAC-MAN[/]")
        );
    }
}
