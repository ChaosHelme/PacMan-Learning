using PacMan.ECS;
using PacMan.Game.Components;
using PacMan.Game.Configuration;
using PacMan.Game.Services;
using Spectre.Console;

namespace PacMan.Game.Rendering;

public class ConsoleRenderingProvider : IRenderingProvider
{
    World _world = null!;
    MazeConfiguration _mazeConfiguration = null!;
    IGameArtAssets _assets = null!;
    IMazeService _mazeService = null!;
    
    public void Initialize(World world, MazeConfiguration mazeConfiguration, IMazeService mazeService, IGameArtAssets assets)
    {
        _world = world;
        _mazeConfiguration = mazeConfiguration;
        _assets = assets;
        _mazeService = mazeService;
    }

	readonly List<(int x, int y)> _ghostPositions = [];
	
    public void Render()
    {
		var playerEntity = _world.GetEntitiesWith<PlayerComponent, PositionComponent>().Single();
		var playerPosition = _world.GetComponent<PositionComponent>(playerEntity).Position;
		var ghostEntities = _world.GetEntitiesWith<GhostComponent, PositionComponent>();
		
		_ghostPositions.Clear();
		foreach (var ghostEntity in ghostEntities)
		{
			var ghostPosition = _world.GetComponent<PositionComponent>(ghostEntity).Position;
			_ghostPositions.Add(ghostPosition);
		}
		
		AnsiConsole.Cursor.SetPosition(0, 0);
        for (var y = 0; y < _mazeConfiguration.Height; y++)
        {
            for (var x = 0; x < _mazeConfiguration.Width; x++)
            {
                var pos = (x, y);
                var dot = _mazeService.IsDotAt(x, y);

                if (_mazeService.IsWallAt(x, y))
                    AnsiConsole.Write(_assets.WallArt);
                else if (playerPosition == pos)
                    AnsiConsole.Write(_assets.PlayerArt);
                else if (_ghostPositions.Contains(pos))
                    AnsiConsole.Write(_assets.GhostArt);
                else if (dot)
                    AnsiConsole.Write(_assets.DotArt);
                else
                    AnsiConsole.Write("  ");
            }
            AnsiConsole.WriteLine();
        }
		
        var score = _world.GetComponent<ScoreComponent>(playerEntity).Score;
        var lives = _world.GetComponent<LivesComponent>(playerEntity).Lives;
        AnsiConsole.WriteLine($"\nScore: {score}   Lives: {lives}");
        AnsiConsole.WriteLine("Use arrow keys to move, Q to quit.");
    }
}