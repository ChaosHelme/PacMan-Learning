namespace PacMan.Game.Services;

public interface IMazeService
{
    bool IsWallAt(int x, int y);
    bool IsDotAt(int x, int y);
    bool IsWalkable(int x, int y);
    bool IsWarpPortal(int x, int y);
    (int X, int Y) GetWarpDestination((int x, int y) source);
}