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
        _mazeService = new MazeService(_world);
        _randomNumberService = new TestRandomNumberService();
        _moveSystem = new GhostMovementSystem(_world, _mazeService, _randomNumberService);
    }

    [Test]
    public void MoveGhosts_GhostMovesToValidPosition()
    {
        var ghost = _world.CreateEntity();
        _world.AddComponent(ghost, new GhostComponent());
        _world.AddComponent(ghost, new PositionComponent((1, 1)));
        
        // We set the TestRandomNumberService to return 1 which is Direction.Right
        _randomNumberService.PreloadRandomNumbers([1]);

        _moveSystem.Execute();
        var positionComponent = _world.GetComponent<PositionComponent>(ghost);
        positionComponent.Position.X.Should().Be(2);
        positionComponent.Position.Y.Should().Be(1);
        _mazeService.IsWalkable(positionComponent.Position.X, positionComponent.Position.Y).Should().BeTrue();
    }

    [Test]
    public void MoveGhosts_GhostDoesntMoveToInvalidPosition()
    {
        var ghost = _world.CreateEntity();
        _world.AddComponent(ghost, new GhostComponent());
        _world.AddComponent(ghost, new PositionComponent((1, 1)));
        
        var wallEntity = _world.CreateEntity();
        _world.AddComponent(wallEntity, new WallComponent());
        _world.AddComponent(wallEntity, new PositionComponent((0, 1)));
        
        // We set the TestRandomNumberService to return 0 which is Direction.Left
        // As we set the Ghost to currently be at position 1,1 - moving to the left would be invalid
        _randomNumberService.PreloadRandomNumbers([0]);
        
        _moveSystem.Execute();
        var positionComponent = _world.GetComponent<PositionComponent>(ghost);
        positionComponent.Position.X.Should().Be(1);
        positionComponent.Position.Y.Should().Be(1);
    }
	
	[Test]
	public void MovePlayer_IntoWarpPortal_ShouldSpawnPlayer_AtTheOppositePosition()
	{
		var ghost = _world.CreateEntity();
		_world.AddComponent(ghost, new GhostComponent());
		_world.AddComponent(ghost, new PositionComponent((1, 1)));
		var warpSourceComponent = _world.CreateEntity();
		_world.AddComponent(warpSourceComponent, new WarpPortalComponent());
		_world.AddComponent(warpSourceComponent, new PositionComponent((0, 1)));
		var warpDestinationComponent = _world.CreateEntity();
		_world.AddComponent(warpDestinationComponent, new WarpPortalComponent());
		_world.AddComponent(warpDestinationComponent, new PositionComponent((2, 1)));
		
		// We set the TestRandomNumberService to return 0 which is Direction.Left
		// As we set the Ghost to currently be at position 1,1 - moving to the left would be invalid
		_randomNumberService.PreloadRandomNumbers([0]);
		_moveSystem.Execute();
		
		_world.GetComponent<PositionComponent>(ghost).Should().Be(new PositionComponent((2, 1)));
	}
}