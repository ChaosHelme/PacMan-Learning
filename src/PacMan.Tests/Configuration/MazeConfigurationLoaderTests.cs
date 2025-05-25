using FluentAssertions;
using PacMan.Game.Configuration;

namespace PacMan.Tests.Configuration;

[TestFixture(TestOf = typeof(MazeConfigurationLoader))]
public class MazeConfigurationLoaderTests
{
    [Test]
    public void LoadMazeConfiguration_LoadsMazeConfiguration_WhenFileExists()
    {
        var testFileService = new TestFileService("##@##\n#...#\n#.# #\n##@##");
        // TestFileService doesn't use a path, it uses the given string from the constructor
        var mazeConfiguration = MazeConfigurationLoader.LoadMazeConfiguration(string.Empty, testFileService);
        
        mazeConfiguration.Height.Should().Be(4);
        mazeConfiguration.Width.Should().Be(5);
        mazeConfiguration.WallCoordinates.Should().HaveCount(13);
        mazeConfiguration.DotCoordinates.Should().HaveCount(4);
		mazeConfiguration.WarpCoordinates.Should().HaveCount(2);
	}

    [Test]
    public void LoadMazeConfiguration_LoadsMazeConfiguration_WhenFileDoesNotExist()
    {
        var testFileService = new TestFileService(string.Empty);
        var mazeConfiguration = MazeConfigurationLoader.LoadMazeConfiguration(string.Empty, testFileService);

        mazeConfiguration.Height.Should().Be(0);
        mazeConfiguration.Width.Should().Be(0);
        mazeConfiguration.WallCoordinates.Should().HaveCount(0);
        mazeConfiguration.DotCoordinates.Should().HaveCount(0);
		mazeConfiguration.WarpCoordinates.Should().HaveCount(0);
    }
}