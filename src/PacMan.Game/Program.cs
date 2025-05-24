using PacMan.Game;
using PacMan.Game.Input;
using PacMan.Game.Menu;
using PacMan.Game.Rendering;

var cts = new CancellationTokenSource();
var app = new PacmanGameApp(
    args,
    new ConsoleInputProvider(),
    new ConsoleRenderingProvider(),
    new MainMenu(),
    new OptionsMenu(),
    cts);
await app.Run();