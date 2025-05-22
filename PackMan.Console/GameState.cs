namespace PackMan.Console;

public class GameState
{
    public Maze Maze { get; }
    public Player Player { get; }
    public List<Ghost> Ghosts { get; }
    public int Score { get; set; }
    public int Lives { get; set; }
    public bool GameOver { get; set; }

    public GameState(Maze maze, Player player, List<Ghost> ghosts, int lives = 3)
    {
        Maze = maze;
        Player = player;
        Ghosts = ghosts;
        Score = 0;
        Lives = lives;
        GameOver = false;
    }
}