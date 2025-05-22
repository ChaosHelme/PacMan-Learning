using Spectre.Console;

namespace PackMan.Console;

public static class ConsoleRenderer
{
    // Emoji definitions
    const string WallEmoji = "🟦";
    const string PlayerEmoji = "🟡";
    const string DotEmoji = "⚪";
    const string GhostEmoji = "👻";
    
    public static void Render(GameState state)
    {
        AnsiConsole.Cursor.SetPosition(0, 0);
        for (var y = 0; y < state.Maze.GetLength(0); y++)
        {
            for (var x = 0; x < state.Maze.GetLength(1); x++)
            {
                var pos = (x, y);

                // Player
                if (state.PlayerPosition == pos)
                {
                    AnsiConsole.Markup(PlayerEmoji);
                }
                // Ghost
                else if (state.Ghosts.Contains(pos))
                {
                    AnsiConsole.Markup(GhostEmoji);
                }
                // Wall
                else if (state.Maze[y, x] == 'W')
                {
                    AnsiConsole.Markup(WallEmoji);
                }
                // Dot
                else if (state.Dots.Contains(pos))
                {
                    AnsiConsole.Markup(DotEmoji);
                }
                // Empty
                else
                {
                    AnsiConsole.Markup("  ");
                }
            }
            AnsiConsole.WriteLine();
        }
        // HUD
        AnsiConsole.MarkupLine($"\nScore: {state.Score}   Lives: {state.Lives}");
    }
}