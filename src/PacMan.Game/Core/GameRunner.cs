using PacMan.ECS;
using PacMan.Game.Components;
using PacMan.Game.Configuration;
using PacMan.Game.Input;
using PacMan.Game.Rendering;
using PacMan.Game.Services;
using PacMan.Game.Systems;
using Spectre.Console;
using IInitializeSystem = PacMan.Game.Systems.IInitializeSystem;

namespace PacMan.Game.Core;

public class GameRunner(
    IRenderingProvider renderingProvider,
    IInputProvider inputProvider,
    IGameArtAssets gameArtAssets,
    MazeConfiguration mazeConfiguration,
    CancellationTokenSource cts) : IGameRunner
{
    public async Task Run(int frameDelay)
    {
        AnsiConsole.Clear();
        AnsiConsole.Cursor.Hide();
        
        // ECS Setup
        var world = new World();
        
        var mazeService = new MazeService(world);
        renderingProvider.Initialize(world, mazeConfiguration, mazeService, gameArtAssets);

        // Entities & Components
        var player = world.CreateEntity();
        world.AddComponent(player, new PlayerComponent());
        world.AddComponent(player, new PositionComponent(1, 1));
        world.AddComponent(player, new DirectionComponent(Direction.None));
        world.AddComponent(player, new ScoreComponent(0));
        world.AddComponent(player, new LivesComponent(3));
        
        var inputEntity = world.CreateEntity();
        world.AddComponent(inputEntity, new InputComponent(Direction.None));

        var ghostPositions = new[] { (18, 13), (18, 1), (1, 13) };
        foreach (var (gx, gy) in ghostPositions)
        {
            var ghost = world.CreateEntity();
            world.AddComponent(ghost, new GhostComponent());
            world.AddComponent(ghost, new PositionComponent(gx, gy));
        }

        ICollection<IInitializeSystem> initializeSystems = [];
        ICollection<IExecuteSystem> executeSystems = [];
        
        // InitializeSystems
        initializeSystems.Add(new MazeLoaderSystem(world, mazeConfiguration));
        
        // ExecuteSystems (Order matters!)
        executeSystems.Add(new InputSystem(world, inputProvider));
        executeSystems.Add(new PlayerDirectionSystem(world));
        executeSystems.Add(new PlayerMovementSystem(world, mazeService));
        executeSystems.Add(new GhostMovementSystem(world, mazeService, new RandomNumberService()));
        executeSystems.Add(new GameLogicSystem(world));

        foreach (var initializeSystem in initializeSystems)
        {
            initializeSystem.Initialize();
        }
        
        // Game Loop
        while (!cts.IsCancellationRequested)
        {
            foreach (var executeSystem in executeSystems)
            {
                executeSystem.Execute();
            }
            
            if (world.GetComponent<InputComponent>(inputEntity).Direction == Direction.Quit)
                break;
            
            renderingProvider.Render();
            
            await Task.Delay(frameDelay);
        }
        
        var finalScore = world.GetComponent<ScoreComponent>(player).Score;
        AnsiConsole.Cursor.SetPosition(0, mazeConfiguration.Height + 2);
        AnsiConsole.Write(
            new Panel($"[bold]Game Over![/]\nFinal Score: {finalScore}")
                .Border(BoxBorder.Rounded)
                .Header("[yellow]PAC-MAN[/]")
        );
        
        AnsiConsole.Cursor.Show();
    }
}