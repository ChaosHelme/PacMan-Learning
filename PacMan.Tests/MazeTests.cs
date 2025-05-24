using FluentAssertions;
using PacMan.Game;

namespace PacMan.Tests;

[TestFixture]
public class MazeTests
{
    private Maze _maze;

    [SetUp]
    public void Setup()
    {
        _maze = new Maze();
    }

    [Test]
    public void IsWallAt_Borders_ReturnsTrue()
    {
        _maze.IsWallAt(0, 0).Should().BeTrue();
        _maze.IsWallAt(Maze.Width - 1, Maze.Height - 1).Should().BeTrue();
    }

    [Test]
    public void IsWalkable_OpenSpace_ReturnsTrue()
    {
        _maze.IsWalkable(1, 1).Should().BeTrue();
    }

    [Test]
    public void RemoveDot_RemovesDot()
    {
        _maze.HasDot(1, 1).Should().BeTrue();
        _maze.RemoveDot(1, 1);
        _maze.HasDot(1, 1).Should().BeFalse();
    }
}