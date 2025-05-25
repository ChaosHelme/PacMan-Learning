namespace PacMan.Game.Services;

public interface IMazeService
{
    bool IsWallAt(int x, int y);
    bool IsDotAt(int x, int y);
    bool IsWalkable(int x, int y);
	bool TryGetWarpDestination(int x, int y, out (int x, int y) destination);
}