namespace PacMan.Game.Menu;

using Spectre.Console;

public class MainMenu : IMenu
{
    public string Show()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(
            new FigletText("Pac-Man Console")
                .Color(Color.Yellow));

        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[green]Main Menu[/]")
                .PageSize(5)
                .HighlightStyle(new Style(foreground: Color.Black, background: Color.Yellow))
                .AddChoices("Start", "Options", "Exit"));

        return choice;
    }
}
