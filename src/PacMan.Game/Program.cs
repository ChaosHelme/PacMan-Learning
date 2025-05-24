using PacMan.Game;
using PacMan.Game.Bootstrap;
using PacMan.Game.Extensions;

var cts = new CancellationTokenSource();

var renderMode = CommandLineOptions.GetRenderMode(args);

var app = new PacmanGameAppBuilder()
    .WithRenderMode(renderMode)
    .WithFrameDelay(100)
    .AddConsoleInputAndRenderProvider()
    .Build();

await app.Start(cts);