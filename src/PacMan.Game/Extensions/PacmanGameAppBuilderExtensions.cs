using PacMan.Game.Bootstrap;
using PacMan.Game.Input;
using PacMan.Game.Rendering;

namespace PacMan.Game.Extensions;

public static class PacmanGameAppBuilderExtensions
{
    public static IPacmanGameAppBuilder AddConsoleInputAndRenderProvider(this IPacmanGameAppBuilder builder)
    {
        builder.WithInputProvider(new ConsoleInputProvider());
        builder.WithRenderingProvider(new ConsoleRenderingProvider());
        return builder;
    }
}