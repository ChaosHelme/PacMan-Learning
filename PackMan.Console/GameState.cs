namespace PackMan.Console;

public sealed class GameState
{
    public int Score { get; set; }
    public int Lives { get; set; } = 3;
    public bool GameOver { get; set; }
    public char[,] Maze { get; init; }
    public (int X, int Y) PlayerPosition { get; set; }
    public List<(int X, int Y)> Ghosts { get; private set; } = [];
    public List<(int X, int Y)> Dots { get; private set; } = [];
    
    public static GameState InitializeGame()
    {
        var maze = new char[15, 20];
        var dots = new List<(int, int)>();
    
        // Create maze layout
        for (var y = 0; y < 15; y++)
        {
            for (var x = 0; x < 20; x++)
            {
                if (x == 0 || x == 19 || y == 0 || y == 14)
                    maze[y, x] = 'W'; // Walls
                else if (x % 2 == 0 && y % 2 == 0)
                    maze[y, x] = 'W';
                else
                {
                    maze[y, x] = 'D'; // Dots
                    dots.Add((x, y));
                }
            }
        }

        return new GameState
        {
            Maze = maze,
            PlayerPosition = (1, 1),
            Ghosts = [(18, 13), (18, 1), (1, 13)],
            Dots = dots
        };
    }
}