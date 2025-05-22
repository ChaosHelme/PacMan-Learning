using Spectre.Console;

namespace PackMan.Console;

public static class GameRenderer
{
    // Emoji Definitions
    private const string WallEmoji = "🟦";
    private const string PlayerEmoji = "🟡";
    private const string DotEmoji = "⚪";
    private const string GhostEmoji = "👻";

    public static void Render(GameState state)
    {
        AnsiConsole.Cursor.SetPosition(0, 0);

        for (int y = 0; y < Maze.Height; y++)
        {
            for (int x = 0; x < Maze.Width; x++)
            {
                var pos = new Position(x, y);

                if (state.Player.Position == pos)
                    AnsiConsole.Write(PlayerEmoji);
                else if (state.Ghosts.Any(g => g.Position == pos))
                    AnsiConsole.Write(GhostEmoji);
                else if (state.Maze.Cells[y, x] == CellType.Wall)
                    AnsiConsole.Write(WallEmoji);
                else if (state.Maze.HasDot(pos))
                    AnsiConsole.Write(DotEmoji);
                else
                    AnsiConsole.Write("  ");
            }
            AnsiConsole.WriteLine();
        }

        AnsiConsole.WriteLine($"\nScore: {state.Score}   Lives: {state.Lives}");
        AnsiConsole.WriteLine("Use arrow keys to move, Q to quit.");
    }

    public static void ShowGameOver(GameState state)
    {
        AnsiConsole.Cursor.SetPosition(0, Maze.Height + 2);
        AnsiConsole.Write(new Panel($"[bold]Game Over![/]\nFinal Score: {state.Score}")
            .Border(BoxBorder.Rounded)
            .Header("[yellow]PAC-MAN[/]"));
    }
}