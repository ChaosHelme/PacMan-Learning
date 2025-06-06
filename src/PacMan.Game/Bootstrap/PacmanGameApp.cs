using PacMan.Game.Configuration;
using PacMan.Game.Core;
using PacMan.Game.Input;
using PacMan.Game.Menu;
using PacMan.Game.Rendering;
using PacMan.Game.Services;

namespace PacMan.Game.Bootstrap;

public class PacmanGameApp
{
    readonly RenderMode _renderMode;
    readonly IInputProvider _inputProvider;
    readonly IRenderingProvider _renderingProvider;
    readonly IMenu _mainMenu;
    readonly IMenu _optionsMenu;
    readonly IFileService _fileService;
    readonly int _frameDelay;

    internal PacmanGameApp(
        RenderMode renderMode,
        IInputProvider inputProvider,
        IRenderingProvider renderingProvider,
        IMenu mainMenu,
        IMenu optionsMenu,
        IFileService fileService,
        int frameDelay)
    {
        _renderMode = renderMode;
        _inputProvider = inputProvider;
        _renderingProvider = renderingProvider;
        _mainMenu = mainMenu;
        _optionsMenu = optionsMenu;
        _frameDelay = frameDelay;
        _fileService = fileService;
    }
    
    public async Task Start(CancellationTokenSource cts)
    {
        IGameArtAssets gameArtAssets = _renderMode == RenderMode.Emoji ? new EmojiConsoleAssets() : new AsciiConsoleAssets();
        var mazeConfiguration = MazeConfigurationLoader.LoadMazeConfiguration(Path.Combine(AppContext.BaseDirectory, "maze.txt"), _fileService);
        var gameRunner = new GameRunner(_renderingProvider, _inputProvider, gameArtAssets, mazeConfiguration, cts);

        // When emoji mode is active, we need to ensure to set the output encoding to UTF8
        if (gameArtAssets is EmojiConsoleAssets)
        {
            System.Console.OutputEncoding = System.Text.Encoding.UTF8;
        }

        while (!cts.IsCancellationRequested)
        {
            var selection = _mainMenu.Show();

            switch (selection)
            {
                case "Start":
                    await gameRunner.Run(_frameDelay);
                    break;
                case "Options":
                    _optionsMenu.Show();
                    break;
                case "Exit":
                    return;
            }
        }
    }
}