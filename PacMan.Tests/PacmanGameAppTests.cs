using PacMan.Ecs.Console;
using FluentAssertions;
using PacMan.Ecs.Console.Rendering;

namespace PacMan.Tests;

[TestFixture]
public class PacmanGameEndToEndTests
{
    [Test]
    public async Task FullGame_WinScenario_PrintsGameOver()
    {
        // Arrange: Simulate user input (e.g., right arrow, right arrow, Q to quit)
        var input = new StringReader($"{(char)ConsoleKey.RightArrow}{(char)ConsoleKey.Enter}{(char)ConsoleKey.Q}{(char)ConsoleKey.Enter}");
        var output = new StringWriter();

        var app = new PacmanGameApp(new TestInputProvider([]), new ConsoleRenderingProvider());

        // Act
        await app.Run();

        var outputAsString = output.ToString();
        // Assert: Check output for expected game over message
        outputAsString.Should().Contain("Score: 10");
    }
}
