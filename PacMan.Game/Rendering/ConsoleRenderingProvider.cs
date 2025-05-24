using PacMan.Game.Components;
using PacMan.Game.Ecs;
using Spectre.Console;

namespace PacMan.Game.Rendering;

public class ConsoleRenderingProvider : IRenderingProvider
{
    World _world = null!;
    Maze _maze = null!;
    IGameArtAssets _assets = null!;
    
    public void Initialize(World world, Maze maze, IGameArtAssets assets)
    {
        _world = world;
        _maze = maze;
        _assets = assets;
    }

    public void Render()
    {
        AnsiConsole.Cursor.SetPosition(0, 0);
        for (var y = 0; y < Maze.Height; y++)
        {
            for (var x = 0; x < Maze.Width; x++)
            {
                var pos = new PositionComponent(x, y);
                var player = _world.GetEntitiesWith<PlayerComponent, PositionComponent>()
                    .FirstOrDefault(e => _world.GetComponent<PositionComponent>(e).Equals(pos));
                var ghost = _world.GetEntitiesWith<GhostComponent, PositionComponent>()
                    .FirstOrDefault(e => _world.GetComponent<PositionComponent>(e).Equals(pos));
                var dot = _maze.HasDot(x, y);

                if (_maze.IsWallAt(x, y))
                    AnsiConsole.Write(_assets.WallArt);
                else if (!player.Equals(default(Entity)) && _world.HasComponent<PlayerComponent>(player))
                    AnsiConsole.Write(_assets.PlayerArt);
                else if (!ghost.Equals(default(Entity)) && _world.HasComponent<GhostComponent>(ghost))
                    AnsiConsole.Write(_assets.GhostArt);
                else if (dot)
                    AnsiConsole.Write(_assets.DotArt);
                else
                    AnsiConsole.Write("  ");
            }
            AnsiConsole.WriteLine();
        }

        var playerEntity = _world.GetEntitiesWith<PlayerComponent>().First();
        var score = _world.GetComponent<ScoreComponent>(playerEntity).Score;
        var lives = _world.GetComponent<LivesComponent>(playerEntity).Lives;
        AnsiConsole.WriteLine($"\nScore: {score}   Lives: {lives}");
        AnsiConsole.WriteLine("Use arrow keys to move, Q to quit.");
    }
}