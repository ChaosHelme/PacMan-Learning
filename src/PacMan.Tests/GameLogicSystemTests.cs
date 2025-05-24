using FluentAssertions;
using PacMan.ECS;
using PacMan.Game;
using PacMan.Game.Components;
using PacMan.Game.Systems;

namespace PacMan.Tests;

[TestFixture]
public class GameLogicSystemTests
{
    private World _world;
    private Maze _maze;
    private GameLogicSystem _logicSystem;
    private Entity _player;

    [SetUp]
    public void Setup()
    {
        _world = new World();
        _maze = new Maze();
        _logicSystem = new GameLogicSystem(_world, _maze);

        _player = _world.CreateEntity();
        _world.AddComponent(_player, new PlayerComponent());
        _world.AddComponent(_player, new PositionComponent(1, 1));
        _world.AddComponent(_player, new ScoreComponent(0));
        _world.AddComponent(_player, new LivesComponent(3));
    }

    [Test]
    public void Process_PlayerCollectsDot_ScoreIncreases()
    {
        // Place a dot at (1,1)
        var dot = _world.CreateEntity();
        _world.AddComponent(dot, new DotComponent());
        _world.AddComponent(dot, new PositionComponent(1, 1));

        _logicSystem.Execute();

        _world.GetComponent<ScoreComponent>(_player).Score.Should().Be(10);
        _maze.HasDot(1, 1).Should().BeFalse();
    }

    [Test]
    public void Process_PlayerCollidesWithGhost_LosesLife()
    {
        _world.AddComponent(_player, new PositionComponent(2, 2));
        var ghost = _world.CreateEntity();
        _world.AddComponent(ghost, new GhostComponent());
        _world.AddComponent(ghost, new PositionComponent(2, 2));

        _logicSystem.Execute();

        _world.GetComponent<LivesComponent>(_player).Lives.Should().Be(2);
    }

    [Test]
    public void Process_PlayerLosesLastLife_GameOver()
    {
        _world.AddComponent(_player, new LivesComponent(1));
        _world.AddComponent(_player, new PositionComponent(2, 2));
        var ghost = _world.CreateEntity();
        _world.AddComponent(ghost, new GhostComponent());
        _world.AddComponent(ghost, new PositionComponent(2, 2));

        _logicSystem.Execute();

        _logicSystem.GameOver.Should().BeTrue();
    }

    [Test]
    public void Process_NoMoreDots_GameOver()
    {
        // Remove all dots
        foreach (var y in Enumerable.Range(0, Maze.Height))
            foreach (var x in Enumerable.Range(0, Maze.Width))
                _maze.RemoveDot(x, y);

        _logicSystem.Execute();

        _logicSystem.GameOver.Should().BeTrue();
    }
}