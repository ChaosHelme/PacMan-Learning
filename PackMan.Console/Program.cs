using PackMan.Console;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.CursorVisible = false;

var maze = new Maze();
var player = new Player(new Position(1, 1));
var ghosts = new List<Ghost>
{
    new(new Position(18, 13)),
    new(new Position(18, 1)),
    new(new Position(1, 13))
};

var state = new GameState(maze, player, ghosts);
var inputHandler = new InputHandler();
var controller = new GameController(state, inputHandler);

await controller.RunAsync();
  