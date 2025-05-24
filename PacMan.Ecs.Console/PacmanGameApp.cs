using PacMan.Ecs.Console.Components;
using PacMan.Ecs.Console.Input;
using PacMan.Ecs.Console.Rendering;
using PacMan.Ecs.Console.Systems;

namespace PacMan.Ecs.Console;

public class PacmanGameApp(IInputProvider inputProvider, IRenderingProvider renderingProvider)
{
    public async Task Run()
    {
        // Use _input.ReadLine()/ReadKey() and _output.WriteLine() instead of Console.ReadLine/WriteLine
        // Game loop here...
        System.Console.Clear();
        System.Console.OutputEncoding = System.Text.Encoding.UTF8;
        System.Console.CursorVisible = false;

        // ECS Setup
        var world = new World();
        var maze = new Maze();
        
        renderingProvider.Initialize(world, maze);

        // Entities & Components
        var player = world.CreateEntity();
        world.AddComponent(player, new PlayerTag());
        world.AddComponent(player, new PositionComponent(1, 1));
        world.AddComponent(player, new ScoreComponent(0));
        world.AddComponent(player, new LivesComponent(3));
        
        var inputEntity = world.CreateEntity();
        world.AddComponent(inputEntity, new InputComponent(Direction.None));

        var ghostPositions = new[] { (18, 13), (18, 1), (1, 13) };
        foreach (var (gx, gy) in ghostPositions)
        {
            var ghost = world.CreateEntity();
            world.AddComponent(ghost, new GhostTag());
            world.AddComponent(ghost, new PositionComponent(gx, gy));
        }

        // Dots
        for (var y = 0; y < Maze.Height; y++)
            for (var x = 0; x < Maze.Width; x++)
            {
                if (!maze.IsWallAt(x, y) && maze.HasDot(x, y))
                {
                    var dot = world.CreateEntity();
                    world.AddComponent(dot, new DotTag());
                    world.AddComponent(dot, new PositionComponent(x, y));
                }
            }

        // Systems
        var inputSystem = new InputSystem(world, inputProvider);
        var playerMovementSystem = new PlayerMovementSystem(world, maze);
        var moveSystem = new MovementSystem(world, maze);
        var logicSystem = new GameLogicSystem(world, maze);
        var renderSystem = new RenderingSystem(world, maze, renderingProvider);

        // Game Loop
        while (!logicSystem.GameOver)
        {
            renderSystem.Render();
            inputSystem.Execute();
            if (world.GetComponent<InputComponent>(inputEntity).Direction == Direction.Quit)
                break;
            
            playerMovementSystem.Execute();
            moveSystem.MoveGhosts();
            logicSystem.Execute();
            
            await Task.Delay(100);
        }
        var finalScore = world.GetComponent<ScoreComponent>(player).Score;
        renderSystem.ShowGameOver(finalScore);
    }
}
