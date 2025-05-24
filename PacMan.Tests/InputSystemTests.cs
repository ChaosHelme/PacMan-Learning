using FluentAssertions;
using PacMan.Game;
using PacMan.Game.Components;
using PacMan.Game.Ecs;
using PacMan.Game.Systems;

namespace PacMan.Tests;

[TestFixture]
public class InputSystemTests
{
    Entity _inputEntity;
    World _world;
    
    [SetUp]
    public void Setup()
    {
        _world = new World();
        _inputEntity = _world.CreateEntity();
        _world.AddComponent(_inputEntity, new InputComponent(Direction.None));
    }
    
    [Test]
    public void PlayerMovesAround_CollectsMultipleDots()
    {
        var testInputs = new Queue<Direction>([
            Direction.Right,
            Direction.Right,
            Direction.Down,
            Direction.Left,
            Direction.Quit,
        ]);
    
        var inputProvider = new TestInputProvider(testInputs);
        var inputSystem = new InputSystem(_world, inputProvider);
        
        inputSystem.Execute();
        _world.GetComponent<InputComponent>(_inputEntity).Direction.Should().Be(Direction.Right);
        inputSystem.Execute();
        _world.GetComponent<InputComponent>(_inputEntity).Direction.Should().Be(Direction.Right);
        inputSystem.Execute();
        _world.GetComponent<InputComponent>(_inputEntity).Direction.Should().Be(Direction.Down);
        inputSystem.Execute();
        _world.GetComponent<InputComponent>(_inputEntity).Direction.Should().Be(Direction.Left);
        inputSystem.Execute();
        _world.GetComponent<InputComponent>(_inputEntity).Direction.Should().Be(Direction.Quit);
    }
}