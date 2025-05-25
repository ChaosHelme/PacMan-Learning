using FluentAssertions;
using PacMan.ECS;
using PacMan.Game.Components;
using PacMan.Game.Configuration;
using PacMan.Game.Systems;

namespace PacMan.Tests.Systems;

[TestFixture, TestOf(typeof(MazeLoaderSystem))]
public class MazeLoaderSystemTests
{
	private World _world;
	private MazeConfiguration _config;
	private MazeLoaderSystem _system;

	[SetUp]
	public void SetUp()
	{
		_world = new World();
		_config = new MazeConfiguration();
		_system = new MazeLoaderSystem(_world, _config);
	}

	[Test]
	public void Initialize_CreatesWallEntitiesWithCorrectComponents()
	{
		_config.WallCoordinates.AddRange([(1, 2), (3, 4)]);
		
		_system.Initialize();

		var wallEntities = _world.GetEntitiesWith<WallComponent>().ToList();
		wallEntities.Should().HaveCount(2);

		foreach (var wallEntity in wallEntities)
		{
			var positionComponent = _world.GetComponent<PositionComponent>(wallEntity);
			positionComponent.Position.Should().BeOneOf(_config.WallCoordinates);
		}
	}

	[Test]
	public void Initialize_CreatesDotEntitiesWithCorrectComponents()
	{
		_config.DotCoordinates.Add((5, 6));
		
		_system.Initialize();

		var dotEntities = _world.GetEntitiesWith<DotComponent>().ToList();
		dotEntities.Should().HaveCount(1);

		var dotEntity = dotEntities.First();
		var positionComponent = _world.GetComponent<PositionComponent>(dotEntity);
		positionComponent.Position.X.Should().Be(5);
		positionComponent.Position.Y.Should().Be(6);
	}

	[Test]
	public void Initialize_CreatesWarpPortalEntitiesWithCorrectComponents()
	{
		_config.WarpCoordinates.AddRange([(7, 8), (9, 10)]);
		
		_system.Initialize();

		var warpEntities = _world.GetEntitiesWith<WarpPortalComponent>().ToList();
		warpEntities.Should().HaveCount(2);

		foreach (var warpEntity in warpEntities)
		{
			var positionComponent = _world.GetComponent<PositionComponent>(warpEntity);
			positionComponent.Position.Should().BeOneOf(_config.WarpCoordinates);
		}
	}
}