using PacMan.ECS;
using PacMan.Game.Components;
using PacMan.Game.Services;

namespace PacMan.Game.Systems;

public class GameLogicSystem : IExecuteSystem
{
    private readonly World _world;
    public bool GameOver { get; private set; }

    public GameLogicSystem(World world)
    {
        _world = world;
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
            var score = _world.GetComponent<ScoreComponent>(player);
            _world.ReplaceComponent(player, new ScoreComponent(score.Score + 10));
            
            _world.DestroyEntity(dot);
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
                _world.ReplaceComponent(player, new PositionComponent((1, 1)));
            }
        }

        // Win condition
        if (!_world.GetEntitiesWith<DotComponent>().Any())
            GameOver = true;
    }
}
