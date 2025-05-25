using FluentAssertions;
using PacMan.ECS;
using PacMan.Game.Components;

namespace PacMan.Tests.ECS;

[TestFixture]
public class WorldTests
{
    private World _world;

    [SetUp]
    public void Setup()
    {
        _world = new World();
    }

    [Test]
    public void CreateEntity_ReturnsUniqueEntities()
    {
        var e1 = _world.CreateEntity();
        var e2 = _world.CreateEntity();
        e1.Should().NotBe(e2);
    }

    [Test]
    public void AddAndGetComponent_ReturnsCorrectComponent()
    {
        var entity = _world.CreateEntity();
        var pos = new PositionComponent((2, 3));
        _world.AddComponent(entity, pos);

        _world.GetComponent<PositionComponent>(entity).Should().Be(pos);
    }

    [Test]
    public void HasComponent_ReturnsTrueIfComponentExists()
    {
        var entity = _world.CreateEntity();
        _world.AddComponent(entity, new PlayerComponent());

        _world.HasComponent<PlayerComponent>(entity).Should().BeTrue();
    }

    [Test]
    public void GetEntitiesWith_ReturnsEntitiesWithComponent()
    {
        var e1 = _world.CreateEntity();
        var e2 = _world.CreateEntity();
        _world.AddComponent(e1, new PlayerComponent());
        _world.AddComponent(e2, new GhostComponent());

        var players = _world.GetEntitiesWith<PlayerComponent>().ToList();
        players.Should().Contain(e1);
        players.Should().NotContain(e2);
    }

    [Test]
    public void RemoveComponent_RemovesComponent()
    {
        var entity = _world.CreateEntity();
        _world.AddComponent(entity, new PlayerComponent());
        _world.RemoveComponent<PlayerComponent>(entity);

        _world.HasComponent<PlayerComponent>(entity).Should().BeFalse();
    }

    [Test]
    public void GetUniqueComponentOwner_Throws_EntityDoesNotExistsException_WhenNoEntityContainsUniqueComponent()
    {
        Assert.Throws<EntityDoesNotExistsException>(() => _world.GetUniqueComponentOwner<PlayerComponent>());
    }

    [Test]
    public void AddUniqueComponent_Throws_InvalidOperationException_WhenUniqueComponentAlreadyExistsOnAnyEntity()
    {
        var e1 = _world.CreateEntity();
        _world.AddComponent(e1, new PlayerComponent());
        var e2 = _world.CreateEntity();
        
        Assert.Throws<InvalidOperationException>(() => _world.AddComponent(e2, new PlayerComponent()));
    }

	[Test]
	public void RemoveUniqueComponent_RemovesComponent()
	{
		var e1 = _world.CreateEntity();
		_world.AddComponent(e1, new PlayerComponent());
		_world.HasComponent<PlayerComponent>(e1).Should().BeTrue();
		_world.RemoveComponent<PlayerComponent>(e1);
		_world.HasComponent<PlayerComponent>(e1).Should().BeFalse();
	}

	[Test]
	public void DestroyEntity_ShouldRemoveAllComponents_AndDestroyEntity()
	{
		var e1 = _world.CreateEntity();
		_world.AddComponent(e1, new PlayerComponent());
		_world.AddComponent(e1, new PositionComponent((2, 3)));
		_world.HasComponent<PositionComponent>(e1).Should().BeTrue();
		_world.HasComponent<PlayerComponent>(e1).Should().BeTrue();
		
		_world.DestroyEntity(e1);
		_world.GetEntitiesWith<PlayerComponent, PositionComponent>().Should().BeEmpty();
	}

    [Test]
    public void AddComponent_Throws_InvalidOperationException_WhenComponentAlreadyExists()
    {
        var e1 = _world.CreateEntity();
        _world.AddComponent(e1, new PositionComponent((2, 3)));
        
        Assert.Throws<InvalidOperationException>(() => _world.AddComponent(e1, new PositionComponent((2, 1))));
    }
}
