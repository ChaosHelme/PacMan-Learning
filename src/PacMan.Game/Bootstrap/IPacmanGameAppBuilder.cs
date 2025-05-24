using PacMan.Game.Input;
using PacMan.Game.Rendering;

namespace PacMan.Game.Bootstrap;

public interface IPacmanGameAppBuilder
{
    IPacmanGameAppBuilder WithRenderMode(RenderMode mode);
    IPacmanGameAppBuilder WithRenderingProvider(IRenderingProvider renderingProvider);
    IPacmanGameAppBuilder WithInputProvider(IInputProvider provider);
    IPacmanGameAppBuilder WithFrameDelay(int milliseconds);
    PacmanGameApp Build();
}