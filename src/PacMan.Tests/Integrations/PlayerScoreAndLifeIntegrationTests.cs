using FluentAssertions;
using PacMan.ECS;
using PacMan.Game;
using PacMan.Game.Components;
using PacMan.Game.Configuration;
using PacMan.Game.Services;
using PacMan.Game.Systems;

namespace PacMan.Tests.Integrations;

[TestFixture]
public class PlayerScoreAndLifeIntegrationTests
{
    private World _world;
    private IMazeService _mazeService;
    private PlayerMovementSystem _playerMoveSystem;
    private GameLogicSystem _logicSystem;
    private Entity _player;

    [SetUp]
    public void Setup()
    {
        _world = new World();
        _mazeService = new MazeService(_world);
        _playerMoveSystem = new PlayerMovementSystem(_world, _mazeService);
        _logicSystem = new GameLogicSystem(_world);

        // Player setup
        _player = _world.CreateEntity();
        _world.AddComponent(_player, new PlayerComponent());
        _world.AddComponent(_player, new PositionComponent(1, 1));
        _world.AddComponent(_player, new ScoreComponent(0));
    }

    [Test]
    public void PlayerEatsDot_ScoreIncreases_AndDotIsRemoved()
    {
        // Place a dot at (2,1)
        var dot = _world.CreateEntity();
        _world.AddComponent(dot, new DotComponent());
        _world.AddComponent(dot, new PositionComponent(2, 1));

        // Move player right to (2,1)
        _world.AddComponent(_player, new DirectionComponent(Direction.Right));
        _playerMoveSystem.Execute();

        // Process game logic (should eat dot)
        _logicSystem.Execute();

        // Assert
        _world.GetComponent<ScoreComponent>(_player).Score.Should().Be(10);
        _world.GetEntitiesWith<DotComponent, PositionComponent>()
            .Any(e => _world.GetComponent<PositionComponent>(e).Equals(new PositionComponent(2, 1)))
            .Should()
            .BeFalse();
    }

    [Test]
    public void PlayerCollidesWithGhost_LosesLife_AndResetsPosition()
    {
        // Place ghost at (1,1)
        var ghost = _world.CreateEntity();
        _world.AddComponent(ghost, new GhostComponent());
        _world.AddComponent(ghost, new PositionComponent(1, 1));
        _world.AddComponent(_player, new LivesComponent(3));

        // Player and ghost at same position
        _logicSystem.Execute();

        _world.GetComponent<LivesComponent>(_player).Lives.Should().Be(2);
        _world.GetComponent<PositionComponent>(_player).Should().Be(new PositionComponent(1, 1));
    }

    [Test]
    public void PlayerLosesAllLives_GameOverIsSet()
    {
        // Place ghost at (1,1)
        var ghost = _world.CreateEntity();
        _world.AddComponent(ghost, new GhostComponent());
        _world.AddComponent(ghost, new PositionComponent(1, 1));
        _world.AddComponent(_player, new LivesComponent(1));

        // Player and ghost at same position
        _logicSystem.Execute();

        _logicSystem.GameOver.Should().BeTrue();
    }
}

