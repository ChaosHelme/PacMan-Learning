using FluentAssertions;
using PacMan.ECS;
using PacMan.Game;
using PacMan.Game.Components;
using PacMan.Game.Services;
using PacMan.Game.Systems;

namespace PacMan.Tests.Integrations;

[TestFixture]
public class PlayerMovementIntegrationTests
{
    Entity _inputEntity;
    World _world;
    InputSystem _inputSystem;
    PlayerMovementSystem _playerMovementSystem;
    PlayerDirectionSystem _playerDirectionSystem;
    TestInputProvider _testInputProvider;
    IMazeService _mazeService;

    [SetUp]
    public void Setup()
    {
        _world = new World();
        _inputEntity = _world.CreateEntity();
        _world.AddComponent(_inputEntity, new InputComponent(Direction.None));

        _mazeService = new MazeService(_world);
        _testInputProvider = new TestInputProvider([]);
        _inputSystem = new InputSystem(_world, _testInputProvider);
        _playerDirectionSystem = new PlayerDirectionSystem(_world);
        _playerMovementSystem = new PlayerMovementSystem(_world, _mazeService);
        
        var player = _world.CreateEntity();
        _world.AddComponent(player, new PlayerComponent());
        _world.AddComponent(player, new PositionComponent(1, 1));
        _world.AddComponent(player, new DirectionComponent(Direction.None));
        _world.AddComponent(player, new ScoreComponent(0));
        _world.AddComponent(player, new LivesComponent(3));
    }

    [Test]
    public void PlayerKeepsMoving_ToLastInputDirection()
    {
        _testInputProvider.AddInput(Direction.Right);
        
        _inputSystem.Execute();
        _playerDirectionSystem.Execute();
        _playerMovementSystem.Execute();

        var playerEntity = _world.GetEntitiesWith<PlayerComponent>().Single();
        var playerPositionComponent = _world.GetComponent<PositionComponent>(playerEntity);
        var inputComponent = _world.GetComponent<InputComponent>(_inputEntity);
        
        inputComponent.Direction.Should().Be(Direction.Right);
        playerPositionComponent.X.Should().Be(2);
        playerPositionComponent.Y.Should().Be(1);
        
        _inputSystem.Execute();
        _playerDirectionSystem.Execute();
        _playerMovementSystem.Execute();
        
        inputComponent = _world.GetComponent<InputComponent>(_inputEntity);
        playerPositionComponent = _world.GetComponent<PositionComponent>(playerEntity);
        
        inputComponent.Direction.Should().Be(Direction.None);
        playerPositionComponent.X.Should().Be(3);
        playerPositionComponent.Y.Should().Be(1);
    }
    
    [Test]
    public void MovePlayer_IntoWall_DoesNotUpdatePosition()
    {
        var wallComponent = _world.CreateEntity();
        _world.AddComponent(wallComponent, new WallComponent());
        _world.AddComponent(wallComponent, new PositionComponent(0, 1));
        
        _testInputProvider.AddInput(Direction.Left);
        
        _inputSystem.Execute();
        _playerDirectionSystem.Execute();
        _playerMovementSystem.Execute();
        
        var playerEntity = _world.GetEntitiesWith<PlayerComponent>().Single();
        
        _world.GetComponent<PositionComponent>(playerEntity).Should().Be(new PositionComponent(1, 1));
    }
}