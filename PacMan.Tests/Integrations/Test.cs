using PacMan.Ecs.Console;
using PacMan.Ecs.Console.Components;
using PacMan.Ecs.Console.Systems;

namespace PacMan.Tests.Integrations;

using NUnit.Framework;
using FluentAssertions;
using System.Linq;

[TestFixture]
public class IntegrationTests
{
    private World _world;
    private Maze _maze;
    private MovementSystem _moveSystem;
    private GameLogicSystem _logicSystem;
    private Entity _player;

    [SetUp]
    public void Setup()
    {
        _world = new World();
        _maze = new Maze();
        _moveSystem = new MovementSystem(_world, _maze);
        _logicSystem = new GameLogicSystem(_world, _maze);

        // Player setup
        _player = _world.CreateEntity();
        _world.AddComponent(_player, new PlayerTag());
        _world.AddComponent(_player, new PositionComponent(1, 1));
        _world.AddComponent(_player, new ScoreComponent(0));
        _world.AddComponent(_player, new LivesComponent(3));
    }

    [Test]
    public void PlayerEatsDot_ScoreIncreases_AndDotIsRemoved()
    {
        // Place a dot at (2,1)
        var dot = _world.CreateEntity();
        _world.AddComponent(dot, new DotTag());
        _world.AddComponent(dot, new PositionComponent(2, 1));
        _maze.RemoveDot(2, 1); // Ensure maze and ECS are in sync
        _maze.RemoveDot(1, 1); // Remove dot from player start

        // Move player right to (2,1)
        //_moveSystem.MovePlayer(Direction.Right);

        // Process game logic (should eat dot)
        _logicSystem.Execute();

        // Assert
        _world.GetComponent<ScoreComponent>(_player).Score.Should().Be(10);
        _world.GetEntitiesWith<DotTag, PositionComponent>()
            .Any(e => _world.GetComponent<PositionComponent>(e).Equals(new PositionComponent(2, 1)))
            .Should()
            .BeFalse();
    }

    [Test]
    public void PlayerCollidesWithGhost_LosesLife_AndResetsPosition()
    {
        // Place ghost at (1,1)
        var ghost = _world.CreateEntity();
        _world.AddComponent(ghost, new GhostTag());
        _world.AddComponent(ghost, new PositionComponent(1, 1));

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
        _world.AddComponent(ghost, new GhostTag());
        _world.AddComponent(ghost, new PositionComponent(1, 1));
        _world.AddComponent(_player, new LivesComponent(1));

        // Player and ghost at same position
        _logicSystem.Execute();

        _logicSystem.GameOver.Should().BeTrue();
    }
}

