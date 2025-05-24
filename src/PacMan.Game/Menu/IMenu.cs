namespace PacMan.Game.Menu;

public interface IMenu
{
    /// <summary>
    /// Shows the menu and returns the user's choice.
    /// </summary>
    /// <returns>A value indicating the menu result or choice.</returns>
    string Show();
}
