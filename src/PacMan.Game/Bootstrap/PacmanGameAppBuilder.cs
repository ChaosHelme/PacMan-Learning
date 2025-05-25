using PacMan.Game.Input;
using PacMan.Game.Menu;
using PacMan.Game.Rendering;
using PacMan.Game.Services;

namespace PacMan.Game.Bootstrap;

public class PacmanGameAppBuilder : IPacmanGameAppBuilder
{
    private RenderMode _renderMode = RenderMode.Ascii;
    private IInputProvider? _inputProvider;
    private IRenderingProvider? _renderingProvider;
    private IFileService? _fileService;
    private int _frameDelay = 100;

    public IPacmanGameAppBuilder WithFileService(IFileService fileService)
    {
        _fileService = fileService;
        return this;
    }
    
    public IPacmanGameAppBuilder WithRenderingProvider(IRenderingProvider renderingProvider)
    {
        _renderingProvider = renderingProvider;
        return this;
    }
    
    public IPacmanGameAppBuilder WithRenderMode(RenderMode mode)
    {
        _renderMode = mode;
        return this;
    }

    public IPacmanGameAppBuilder WithInputProvider(IInputProvider provider)
    {
        _inputProvider = provider;
        return this;
    }

    public IPacmanGameAppBuilder WithFrameDelay(int milliseconds)
    {
        _frameDelay = milliseconds;
        return this;
    }

    public PacmanGameApp Build()
    {
        return new PacmanGameApp(
            _renderMode,
            _inputProvider ?? new ConsoleInputProvider(),
            _renderingProvider ?? new ConsoleRenderingProvider(),
            new MainMenu(),
            new OptionsMenu(),
            _fileService ?? new FileService(),
            _frameDelay
        );
    }
}