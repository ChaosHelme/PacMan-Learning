using FluentAssertions;
using PacMan.ECS;
using PacMan.Game.Components;
using PacMan.Game.Services;

namespace PacMan.Tests.Services
{
    [TestFixture]
    public class MazeServiceTests
    {
        private World _world;
        private MazeService _mazeService;

        [SetUp]
        public void SetUp()
        {
            _world = new World();
            _mazeService = new MazeService(_world);
        }

        [Test]
        public void IsWallAt_ReturnsTrue_IfWallExistsAtCoordinates()
        {
            var wall = _world.CreateEntity();
            _world.AddComponent(wall, new WallComponent());
            _world.AddComponent(wall, new PositionComponent((2, 3)));

            _mazeService.IsWallAt(2, 3).Should().BeTrue();
        }

        [Test]
        public void IsWallAt_ReturnsFalse_IfNoWallExistsAtCoordinates()
        {
            var wall = _world.CreateEntity();
            _world.AddComponent(wall, new WallComponent());
            _world.AddComponent(wall, new PositionComponent((1, 1)));

            _mazeService.IsWallAt(2, 2).Should().BeFalse();
        }

        [Test]
        public void IsDotAt_ReturnsTrue_IfDotExistsAtCoordinates()
        {
            var dot = _world.CreateEntity();
            _world.AddComponent(dot, new DotComponent());
            _world.AddComponent(dot, new PositionComponent((5, 6)));

            _mazeService.IsDotAt(5, 6).Should().BeTrue();
        }

        [Test]
        public void IsDotAt_ReturnsFalse_IfNoDotExistsAtCoordinates()
        {
            var dot = _world.CreateEntity();
            _world.AddComponent(dot, new DotComponent());
            _world.AddComponent(dot, new PositionComponent((0, 0)));

            _mazeService.IsDotAt(1, 1).Should().BeFalse();
        }

        [Test]
        public void IsWalkable_ReturnsFalse_IfWallAtCoordinates()
        {
            var wall = _world.CreateEntity();
            _world.AddComponent(wall, new WallComponent());
            _world.AddComponent(wall, new PositionComponent((8, 9)));

            _mazeService.IsWalkable(8, 9).Should().BeFalse();
        }

        [Test]
        public void IsWalkable_ReturnsTrue_IfNoWallAtCoordinates()
        {
            _mazeService.IsWalkable(10, 10).Should().BeTrue();
        }

        [Test]
        public void IsWallAt_And_IsDotAt_WorkIndependently()
        {
            var wall = _world.CreateEntity();
            _world.AddComponent(wall, new WallComponent());
            _world.AddComponent(wall, new PositionComponent((2, 2)));

            var dot = _world.CreateEntity();
            _world.AddComponent(dot, new DotComponent());
            _world.AddComponent(dot, new PositionComponent((3, 3)));

            _mazeService.IsWallAt(2, 2).Should().BeTrue();
            _mazeService.IsDotAt(2, 2).Should().BeFalse();

            _mazeService.IsDotAt(3, 3).Should().BeTrue();
            _mazeService.IsWallAt(3, 3).Should().BeFalse();
        }
		
		[Test]
		public void TryGetWarpDestination_Returns_CorrectDestination_WhenWarpPortalContainsSourceAndDestination()
		{
			// Arrange: add two warp entities in row 5 at (0,5) and (27,5)
			var leftWarp = _world.CreateEntity();
			_world.AddComponent(leftWarp, new WarpPortalComponent());
			_world.AddComponent(leftWarp, new PositionComponent((0, 5)));
			var rightWarp = _world.CreateEntity();
			_world.AddComponent(rightWarp, new WarpPortalComponent());
			_world.AddComponent(rightWarp, new PositionComponent((27, 5)));

			var mazeService = new MazeService(_world);

			// Act
			var result = mazeService.TryGetWarpDestination(0, 5, out var newPos);

			// Assert: newPos should be (27,5) and result should be true
			result.Should().BeTrue();
			newPos.Should().Be((27, 5));
		}
    }
}
