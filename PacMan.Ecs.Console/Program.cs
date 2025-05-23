using PacMan.Ecs.Console;
using PacMan.Ecs.Console.Components;
using PacMan.Ecs.Console.Systems;

Console.Clear();
Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.CursorVisible = false;

// ECS Setup
var world = new World();
var maze = new Maze();

// Entities & Components
var player = world.CreateEntity();
world.AddComponent(player, new PlayerTag());
world.AddComponent(player, new PositionComponent(1, 1));
world.AddComponent(player, new ScoreComponent(0));
world.AddComponent(player, new LivesComponent(3));

var ghostPositions = new[] { (18, 13), (18, 1), (1, 13) };
foreach (var (gx, gy) in ghostPositions)
{
    var ghost = world.CreateEntity();
    world.AddComponent(ghost, new GhostTag());
    world.AddComponent(ghost, new PositionComponent(gx, gy));
}

// Dots
for (int y = 0; y < Maze.Height; y++)
    for (int x = 0; x < Maze.Width; x++)
    {
        if (!maze.IsWallAt(x, y) && maze.HasDot(x, y))
        {
            var dot = world.CreateEntity();
            world.AddComponent(dot, new DotTag());
            world.AddComponent(dot, new PositionComponent(x, y));
        }
    }

// Systems
var inputSystem = new InputSystem();
var moveSystem = new MovementSystem(world, maze);
var logicSystem = new GameLogicSystem(world, maze);
var renderSystem = new RenderingSystem(world, maze);

// Game Loop
while (!logicSystem.GameOver)
{
    renderSystem.Render();
    inputSystem.Poll();
    if (inputSystem.LastDirection == Direction.Quit)
        break;
    moveSystem.MovePlayer(inputSystem.LastDirection);
    moveSystem.MoveGhosts();
    logicSystem.Process();
    await Task.Delay(100);
}
var finalScore = world.GetComponent<ScoreComponent>(player).Score;
renderSystem.ShowGameOver(finalScore);