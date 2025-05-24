using FluentAssertions;
using PacMan.ECS;
using PacMan.Game;
using PacMan.Game.Components;
using PacMan.Game.Systems;

namespace PacMan.Tests;

[TestFixture]
public class GhostMovementSystemTests
{
    private World _world;
    private Maze _maze;
    private GhostMovementSystem _moveSystem;
    private Entity _player;

    [SetUp]
    public void Setup()
    {
        _world = new World();
        _maze = new Maze();
        _moveSystem = new GhostMovementSystem(_world, _maze, new TestRandomNumberService());

        _player = _world.CreateEntity();
        _world.AddComponent(_player, new PlayerComponent());
        _world.AddComponent(_player, new PositionComponent(1, 1));
    }

    [Test]
    public void MoveGhosts_GhostMovesToValidPosition()
    {
        var ghost = _world.CreateEntity();
        _world.AddComponent(ghost, new GhostComponent());
        _world.AddComponent(ghost, new PositionComponent(1, 1));

        _moveSystem.Execute();
        var pos = _world.GetComponent<PositionComponent>(ghost);
        _maze.IsWalkable(pos.X, pos.Y).Should().BeTrue();
    }
}