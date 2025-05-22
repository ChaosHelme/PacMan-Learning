using System.Text;
using System.Threading.Channels;
using PackMan.Console;
using Spectre.Console;

Console.OutputEncoding = Encoding.UTF8;
Console.CursorVisible = false;

// Simple ghost movement (random)
var random = new Random(DateTime.Now.Millisecond);
var state = GameState.InitializeGame();
var inputChannel = Channel.CreateUnbounded<ConsoleKey>();

// Input handling thread
_ = Task.Run(async () =>
{
    while (!state.GameOver)
    {
        var key = Console.ReadKey(true).Key;
        await inputChannel.Writer.WriteAsync(key);
    }
});

// Game loop
while (!state.GameOver)
{
    ConsoleRenderer.Render(state);
    
    if (inputChannel.Reader.TryRead(out var key))
    {
        var (dx, dy) = key switch
        {
            ConsoleKey.LeftArrow => (-1, 0),
            ConsoleKey.RightArrow => (1, 0),
            ConsoleKey.UpArrow => (0, -1),
            ConsoleKey.DownArrow => (0, 1),
            _ => (0, 0)
        };

        // Update player position
        var newX = state.PlayerPosition.X + dx;
        var newY = state.PlayerPosition.Y + dy;
        
        if (state.Maze[newY, newX] != 'W')
        {
            state.PlayerPosition = (newX, newY);
            
            // Collect dots
            if (state.Dots.Remove(state.PlayerPosition))
                state.Score += 10;
        }
    }
    
    for (var i = 0; i < state.Ghosts.Count; i++)
    {
        var (x, y) = state.Ghosts[i];
        var direction = random.Next(0, 4);
        
        var newGhostPos = direction switch
        {
            0 => (x + 1, y),
            1 => (x - 1, y),
            2 => (x, y + 1),
            3 => (x, y - 1),
            _ => (x, y)
        };

        if (state.Maze[newGhostPos.Item2, newGhostPos.Item1] != 'W')
            state.Ghosts[i] = newGhostPos;
    }

    // Check collisions
    if (state.Ghosts.Contains(state.PlayerPosition))
    {
        state.Lives--;
        if (state.Lives <= 0)
            state.GameOver = true;
        else
            state.PlayerPosition = (1, 1);
    }

    // Win condition
    if (state.Dots.Count == 0)
        state.GameOver = true;

    await Task.Delay(100);
}

AnsiConsole.Clear();
AnsiConsole.Write(new Panel($"[bold]Game Over![/]\nFinal Score: {state.Score}")
    .Border(BoxBorder.Rounded)
    .Header("[yellow]PAC-MAN[/]"));