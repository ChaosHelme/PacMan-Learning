namespace PackMan.Console;

public interface IActor
{
    Position Position { get; set; }
    void Move(Direction direction, Maze maze);
}