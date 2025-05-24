using Spectre.Console;

namespace PacMan.Game.Menu;

public class OptionsMenu : IMenu
{
    public string Show()
    {
        AnsiConsole.MarkupLine("[yellow]Options menu coming soon![/]");
        AnsiConsole.MarkupLine("Press any key to return to the main menu...");
        Console.ReadKey(true);
        return "Back";
    }
}