using PacMan.Ecs.Console;
using PacMan.Ecs.Console.Input;
using PacMan.Ecs.Console.Rendering;

var app = new PacmanGameApp(new ConsoleInputProvider(), new ConsoleRenderingProvider());
await app.Run();