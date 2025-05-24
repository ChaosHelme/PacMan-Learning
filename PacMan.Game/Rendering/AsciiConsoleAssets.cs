namespace PacMan.Game.Rendering;

public class AsciiConsoleAssets : IGameArtAssets
{
    public string WallArt => "# ";
    public string PlayerArt => "@ ";
    public string DotArt => "• ";
    public string GhostArt => "ƒ ";
}