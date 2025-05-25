using FluentAssertions;
using PacMan.Game.Input;
using PacMan.Game.Systems;

namespace PacMan.Tests.Input;

[TestFixture(TestOf = typeof(InputMapper))]
public class InputMapperTest
{
	[Test]
	public void InputMapper_ReturnsCorrect_Direction_ForArrowKeys()
	{
		InputMapper.MapKey(ConsoleKey.LeftArrow).Should().Be(Direction.Left);
		InputMapper.MapKey(ConsoleKey.RightArrow).Should().Be(Direction.Right);
		InputMapper.MapKey(ConsoleKey.UpArrow).Should().Be(Direction.Up);
		InputMapper.MapKey(ConsoleKey.DownArrow).Should().Be(Direction.Down);
	}

	[Test]
	public void InputMapper_ReturnsCorrect_Direction_ForWASDKeys()
	{
		InputMapper.MapKey(ConsoleKey.A).Should().Be(InputMapper.MapKey(ConsoleKey.LeftArrow));
		InputMapper.MapKey(ConsoleKey.D).Should().Be(InputMapper.MapKey(ConsoleKey.RightArrow));
		InputMapper.MapKey(ConsoleKey.W).Should().Be(InputMapper.MapKey(ConsoleKey.UpArrow));
		InputMapper.MapKey(ConsoleKey.S).Should().Be(InputMapper.MapKey(ConsoleKey.DownArrow));
	}

	[Test]
	public void InputMapper_Returns_Quit_ForQKey()
	{
		InputMapper.MapKey(ConsoleKey.Q).Should().Be(Direction.Quit);
	}
	
	[Test]
	public void InputMapper_Returns_DirectionNone_ForNonMappedKey()
	{
		InputMapper.MapKey(ConsoleKey.Y).Should().Be(Direction.None);
	}
}