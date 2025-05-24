using PacMan.Game.Core;
using PacMan.Game.Input;
using PacMan.Game.Menu;
using PacMan.Game.Rendering;

namespace PacMan.Game;

public class PacmanGameApp(
    string[] args,
    IInputProvider inputProvider, 
    IRenderingProvider renderingProvider,
    IMenu mainMenu,
    IMenu optionsMenu,
    CancellationTokenSource cts)
{
    public async Task Run()
    {
        IGameArtAssets gameArtAssets = CommandLineOptions.GetRenderMode(args) == RenderMode.Emoji 
            ? new EmojiConsoleAssets() 
            : new AsciiConsoleAssets();
        
        var gameRunner = new GameRunner(renderingProvider, inputProvider, gameArtAssets, cts);

        // When emoji mode is active, we need to ensure to set the output encoding to UTF8
        if (gameArtAssets is EmojiConsoleAssets)
        {
            System.Console.OutputEncoding = System.Text.Encoding.UTF8;
        }

        while (!cts.IsCancellationRequested)
        {
            var selection = mainMenu.Show();

            switch (selection)
            {
                case "Start":
                    await gameRunner.Run();
                    break;
                case "Options":
                    optionsMenu.Show();
                    break;
                case "Exit":
                    return;
            }
        }
    }
}