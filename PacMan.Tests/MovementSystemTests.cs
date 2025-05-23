using FluentAssertions;
using PacMan.Ecs.Console;
using PacMan.Ecs.Console.Components;
using PacMan.Ecs.Console.Systems;

namespace PacMan.Tests;

[TestFixture]
public class MovementSystemTests
{
    private World _world;
    private Maze _maze;
    private MovementSystem _moveSystem;
    private Entity _player;

    [SetUp]
    public void Setup()
    {
        _world = new World();
        _maze = new Maze();
        _moveSystem = new MovementSystem(_world, _maze);

        _player = _world.CreateEntity();
        _world.AddComponent(_player, new PlayerTag());
        _world.AddComponent(_player, new PositionComponent(1, 1));
    }

    [Test]
    public void MovePlayer_ValidDirection_UpdatesPosition()
    {
        _moveSystem.MovePlayer(Direction.Right);

        _world.GetComponent<PositionComponent>(_player).Should().Be(new PositionComponent(2, 1));
    }

    [Test]
    public void MovePlayer_IntoWall_DoesNotUpdatePosition()
    {
        _world.AddComponent(_player, new PositionComponent(0, 1)); // At wall
        _moveSystem.MovePlayer(Direction.Left);

        _world.GetComponent<PositionComponent>(_player).Should().Be(new PositionComponent(0, 1));
    }

    [Test]
    public void MoveGhosts_GhostMovesToValidPosition()
    {
        var ghost = _world.CreateEntity();
        _world.AddComponent(ghost, new GhostTag());
        _world.AddComponent(ghost, new PositionComponent(1, 1));

        _moveSystem.MoveGhosts();
        var pos = _world.GetComponent<PositionComponent>(ghost);
        _maze.IsWalkable(pos.X, pos.Y).Should().BeTrue();
    }
}