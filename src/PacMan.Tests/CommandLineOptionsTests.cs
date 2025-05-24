using FluentAssertions;
using PacMan.Game;

namespace PacMan.Tests
{
    [TestFixture]
    public class CommandLineOptionsTests
    {
        [Test]
        public void GetRenderMode_DefaultsToAscii_WhenNoArgs()
        {
            var args = Array.Empty<string>();
            CommandLineOptions.GetRenderMode(args).Should().Be(RenderMode.Ascii);
        }

        [Test]
        public void GetRenderMode_ParsesEmoji_WithEqualsSyntax()
        {
            var args = new[] { "--render-mode=emoji" };
            CommandLineOptions.GetRenderMode(args).Should().Be(RenderMode.Emoji);
        }

        [Test]
        public void GetRenderMode_ParsesAscii_WithEqualsSyntax()
        {
            var args = new[] { "--render-mode=ascii" };
            CommandLineOptions.GetRenderMode(args).Should().Be(RenderMode.Ascii);
        }

        [Test]
        public void GetRenderMode_ParsesEmoji_WithSpaceSyntax()
        {
            var args = new[] { "--render-mode", "emoji" };
            CommandLineOptions.GetRenderMode(args).Should().Be(RenderMode.Emoji);
        }

        [Test]
        public void GetRenderMode_ParsesAscii_WithSpaceSyntax()
        {
            var args = new[] { "--render-mode", "ascii" };
            CommandLineOptions.GetRenderMode(args).Should().Be(RenderMode.Ascii);
        }

        [Test]
        public void GetRenderMode_IsCaseInsensitive()
        {
            var args = new[] { "--render-mode", "EmOjI" };
            CommandLineOptions.GetRenderMode(args).Should().Be(RenderMode.Emoji);
        }

        [Test]
        public void GetRenderMode_ReturnsAscii_OnUnknownValue()
        {
            var args = new[] { "--render-mode", "unknown" };
            CommandLineOptions.GetRenderMode(args).Should().Be(RenderMode.Ascii);
        }

        [Test]
        public void GetRenderMode_ReturnsAscii_IfNoValueProvided()
        {
            var args = new[] { "--render-mode" };
            CommandLineOptions.GetRenderMode(args).Should().Be(RenderMode.Ascii);
        }

        [Test]
        public void GetRenderMode_UsesFirstOccurrence()
        {
            var args = new[] { "--render-mode=emoji", "--render-mode=ascii" };
            CommandLineOptions.GetRenderMode(args).Should().Be(RenderMode.Emoji);
        }
    }
}
