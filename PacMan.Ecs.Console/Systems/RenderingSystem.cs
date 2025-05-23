using PacMan.Ecs.Console.Components;
using Spectre.Console;

namespace PacMan.Ecs.Console.Systems;

public class RenderingSystem
{
    private readonly World _world;
    private readonly Maze _maze;
    private const string WallEmoji = "🟦";
    private const string PlayerEmoji = "🟡";
    private const string DotEmoji = "⚪";
    private const string GhostEmoji = "👻";

    public RenderingSystem(World world, Maze maze)
    {
        _world = world;
        _maze = maze;
    }

    public void Render()
    {
        AnsiConsole.Cursor.SetPosition(0, 0);
        for (int y = 0; y < Maze.Height; y++)
        {
            for (int x = 0; x < Maze.Width; x++)
            {
                var pos = new PositionComponent(x, y);
                var player = _world.GetEntitiesWith<PlayerTag, PositionComponent>()
                    .FirstOrDefault(e => _world.GetComponent<PositionComponent>(e).Equals(pos));
                var ghost = _world.GetEntitiesWith<GhostTag, PositionComponent>()
                    .FirstOrDefault(e => _world.GetComponent<PositionComponent>(e).Equals(pos));
                var dot = _maze.HasDot(x, y);

                if (_maze.IsWallAt(x, y))
                    AnsiConsole.Write(WallEmoji);
                else if (!player.Equals(default(Entity)) && _world.HasComponent<PlayerTag>(player))
                    AnsiConsole.Write(PlayerEmoji);
                else if (!ghost.Equals(default(Entity)) && _world.HasComponent<GhostTag>(ghost))
                    AnsiConsole.Write(GhostEmoji);
                else if (dot)
                    AnsiConsole.Write(DotEmoji);
                else
                    AnsiConsole.Write("  ");
            }
            AnsiConsole.WriteLine();
        }

        var playerEntity = _world.GetEntitiesWith<PlayerTag>().First();
        var score = _world.GetComponent<ScoreComponent>(playerEntity).Score;
        var lives = _world.GetComponent<LivesComponent>(playerEntity).Lives;
        AnsiConsole.WriteLine($"\nScore: {score}   Lives: {lives}");
        AnsiConsole.WriteLine("Use arrow keys to move, Q to quit.");
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
