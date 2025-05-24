using PacMan.Game;
using PacMan.Game.Input;
using PacMan.Game.Rendering;

var cts = new CancellationTokenSource();
var app = new PacmanGameApp(args, new ConsoleInputProvider(), new ConsoleRenderingProvider(), cts);
await app.Run();