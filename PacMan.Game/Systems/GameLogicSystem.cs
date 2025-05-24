using PacMan.Game.Components;
using PacMan.Game.Ecs;

namespace PacMan.Game.Systems;

public class GameLogicSystem : IExecuteSystem
{
    private readonly World _world;
    private readonly Maze _maze;
    public bool GameOver { get; private set; }

    public GameLogicSystem(World world, Maze maze)
    {
        _world = world;
        _maze = maze;
    }

    public void Execute()
    {
        var player = _world.GetEntitiesWith<PlayerComponent, PositionComponent>().First();
        var playerPos = _world.GetComponent<PositionComponent>(player);

        // Collect dot
        var dot = _world.GetEntitiesWith<DotComponent, PositionComponent>()
            .FirstOrDefault(e => _world.GetComponent<PositionComponent>(e).Equals(playerPos));
        if (!dot.Equals(default(Entity)))
        {
            _world.RemoveComponent<DotComponent>(dot);
            _maze.RemoveDot(playerPos.X, playerPos.Y);
            var score = _world.GetComponent<ScoreComponent>(player);
            _world.ReplaceComponent(player, new ScoreComponent(score.Score + 10));
        }

        // Ghost collision
        var ghosts = _world.GetEntitiesWith<GhostComponent, PositionComponent>();
        if (ghosts.Any(g => _world.GetComponent<PositionComponent>(g).Equals(playerPos)))
        {
            var lives = _world.GetComponent<LivesComponent>(player);
            if (lives.Lives <= 1)
            {
                GameOver = true;
            }
            else
            {
                _world.ReplaceComponent(player, new LivesComponent(lives.Lives - 1));
                _world.ReplaceComponent(player, new PositionComponent(1, 1));
            }
        }

        // Win condition
        if (!_world.GetEntitiesWith<DotComponent>().Any())
            GameOver = true;
    }
}
