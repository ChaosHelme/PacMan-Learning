using PacMan.Ecs.Console.Components;

namespace PacMan.Ecs.Console.Systems;

public class GameLogicSystem
{
    private readonly World _world;
    private readonly Maze _maze;
    public bool GameOver { get; private set; }

    public GameLogicSystem(World world, Maze maze)
    {
        _world = world;
        _maze = maze;
    }

    public void Process()
    {
        var player = _world.GetEntitiesWith<PlayerTag, PositionComponent>().First();
        var playerPos = _world.GetComponent<PositionComponent>(player);

        // Collect dot
        var dot = _world.GetEntitiesWith<DotTag, PositionComponent>()
            .FirstOrDefault(e => _world.GetComponent<PositionComponent>(e).Equals(playerPos));
        if (!dot.Equals(default(Entity)))
        {
            _world.RemoveComponent<DotTag>(dot);
            _maze.RemoveDot(playerPos.X, playerPos.Y);
            var score = _world.GetComponent<ScoreComponent>(player);
            _world.AddComponent(player, score with { Score = score.Score + 10 });
        }

        // Ghost collision
        var ghosts = _world.GetEntitiesWith<GhostTag, PositionComponent>();
        if (ghosts.Any(g => _world.GetComponent<PositionComponent>(g).Equals(playerPos)))
        {
            var lives = _world.GetComponent<LivesComponent>(player);
            if (lives.Lives <= 1)
            {
                GameOver = true;
            }
            else
            {
                _world.AddComponent(player, lives with { Lives = lives.Lives - 1 });
                _world.AddComponent(player, new PositionComponent(1, 1));
            }
        }

        // Win condition
        if (!_world.GetEntitiesWith<DotTag>().Any())
            GameOver = true;
    }
}
