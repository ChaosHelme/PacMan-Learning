namespace PackMan.Console;

public class GameController
{
    private readonly GameState _state;
    private readonly InputHandler _inputHandler;

    public GameController(GameState state, InputHandler inputHandler)
    {
        _state = state;
        _inputHandler = inputHandler;
    }

    public async Task RunAsync()
    {
        while (!_state.GameOver)
        {
            GameRenderer.Render(_state);
            HandleInput();
            MoveGhosts();
            CheckCollisions();
            await Task.Delay(100);
        }

        GameRenderer.ShowGameOver(_state);
    }

    private void HandleInput()
    {
        var direction = _inputHandler.GetDirection();
        if (direction == Direction.Quit)
        {
            _state.GameOver = true;
            return;
        }

        if (direction != Direction.None)
        {
            _state.Player.Move(direction, _state.Maze);

            // Collect dot if present
            if (_state.Maze.HasDot(_state.Player.Position))
            {
                _state.Maze.RemoveDot(_state.Player.Position);
                _state.Score += 10;
            }
        }
    }

    private void MoveGhosts()
    {
        foreach (var ghost in _state.Ghosts)
            ghost.MoveRandom(_state.Maze);
    }

    private void CheckCollisions()
    {
        // Collision with ghost
        if (_state.Ghosts.Any(g => g.Position == _state.Player.Position))
        {
            _state.Lives--;
            if (_state.Lives <= 0)
            {
                _state.GameOver = true;
            }
            else
            {
                _state.Player.Position = new Position(1, 1);
            }
        }

        // Win condition
        if (_state.Maze.AllDotsCollected())
            _state.GameOver = true;
    }
}