using FluentAssertions;
using PacMan.ECS;
using PacMan.Game;
using PacMan.Game.Components;
using PacMan.Game.Configuration;
using PacMan.Game.Services;
using PacMan.Game.Systems;
using PacMan.Tests.Services;

namespace PacMan.Tests.Systems;

[TestFixture]
public class GhostMovementSystemTests
{
    private World _world;
    private IMazeService _mazeService;
    private GhostMovementSystem _moveSystem;
    private TestRandomNumberService _randomNumberService;

    [SetUp]
    public void Setup()
    {
        _world = new World();
        _mazeService = new MazeService(_world, new MazeConfiguration());
        _randomNumberService = new TestRandomNumberService();
        _moveSystem = new GhostMovementSystem(_world, _mazeService, _randomNumberService);
    }

    [Test]
    public void MoveGhosts_GhostMovesToValidPosition()
    {
        var ghost = _world.CreateEntity();
        _world.AddComponent(ghost, new GhostComponent());
        _world.AddComponent(ghost, new PositionComponent(1, 1));
        
        // We set the TestRandomNumberService to return 1 which is Direction.Right
        _randomNumberService.PreloadRandomNumbers([1]);

        _moveSystem.Execute();
        var pos = _world.GetComponent<PositionComponent>(ghost);
        pos.X.Should().Be(2);
        pos.Y.Should().Be(1);
        _mazeService.IsWalkable(pos.X, pos.Y).Should().BeTrue();
    }

    [Test]
    public void MoveGhosts_GhostDoesntMoveToInvalidPosition()
    {
        var ghost = _world.CreateEntity();
        _world.AddComponent(ghost, new GhostComponent());
        _world.AddComponent(ghost, new PositionComponent(1, 1));
        
        var wallEntity = _world.CreateEntity();
        _world.AddComponent(wallEntity, new WallComponent());
        _world.AddComponent(wallEntity, new PositionComponent(0, 1));
        
        // We set the TestRandomNumberService to return 0 which is Direction.Left
        // As we set the Ghost to currently be at position 1,1 - moving to the left would be invalid
        _randomNumberService.PreloadRandomNumbers([0]);
        
        _moveSystem.Execute();
        var pos = _world.GetComponent<PositionComponent>(ghost);
        pos.X.Should().Be(1);
        pos.Y.Should().Be(1);
    }
}