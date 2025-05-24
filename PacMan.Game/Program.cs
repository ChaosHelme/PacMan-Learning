using PacMan.Game;
using PacMan.Game.Input;
using PacMan.Game.Rendering;

var app = new PacmanGameApp(new ConsoleInputProvider(), new ConsoleRenderingProvider());
await app.Run();