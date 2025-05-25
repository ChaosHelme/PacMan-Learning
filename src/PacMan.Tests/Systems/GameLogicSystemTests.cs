using FluentAssertions;
using PacMan.ECS;
using PacMan.Game;
using PacMan.Game.Components;
using PacMan.Game.Systems;

namespace PacMan.Tests.Systems;

[TestFixture]
public class GameLogicSystemTests
{
    private World _world;
    private GameLogicSystem _logicSystem;
    private Entity _player;

    [SetUp]
    public void Setup()
    {
        _world = new World();
        _logicSystem = new GameLogicSystem(_world);

        _player = _world.CreateEntity();
        _world.AddComponent(_player, new PlayerComponent());
        _world.AddComponent(_player, new ScoreComponent(0));
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
        _world.AddComponent(_player, new PositionComponent(2, 2));
        
        _logicSystem.Execute();
        
        _logicSystem.GameOver.Should().BeTrue();
    }
}