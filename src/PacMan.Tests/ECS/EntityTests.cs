using FluentAssertions;
using PacMan.ECS;

namespace PacMan.Tests.ECS;

[TestFixture]
public class EntityTests
{
    [Test]
    public void Entities_WithSameId_AreEqual()
    {
        var e1 = new Entity(42);
        var e2 = new Entity(42);

        e1.Should().Be(e2);
        (e1 == e2).Should().BeTrue();
        (e1 != e2).Should().BeFalse();
        e1.Equals(e2).Should().BeTrue();
        e1.Equals((object) e2).Should().BeTrue();
    }

    [Test]
    public void Entities_WithDifferentIds_AreNotEqual()
    {
        var e1 = new Entity(1);
        var e2 = new Entity(2);

        e1.Should().NotBe(e2);
        (e1 == e2).Should().BeFalse();
        (e1 != e2).Should().BeTrue();
        e1.Equals(e2).Should().BeFalse();
        e1.Equals((object) e2).Should().BeFalse();
    }

    [Test]
    public void Equals_OnNonEntityObject_ReturnsFalse()
    {
        var e1 = new Entity(1);
        var nonEntity = new object();
        
        e1.Equals(nonEntity).Should().BeFalse();
    }

    [Test]
    public void GetHashCode_ReturnsId()
    {
        var id = 123;
        var entity = new Entity(id);

        entity.GetHashCode().Should().Be(id);
    }

    [Test]
    public void Equals_WithNullObject_ReturnsFalse()
    {
        var entity = new Entity(1);

        entity.Equals(null).Should().BeFalse();
    }

    [Test]
    public void DefaultEntity_HasIdZero()
    {
        var entity = default(Entity);

        entity.Id.Should().Be(0);
    }

    [Test]
    public void Entities_CanBeUsedInHashSet()
    {
        var set = new HashSet<Entity>();
        var e1 = new Entity(10);
        var e2 = new Entity(10);
        var e3 = new Entity(11);

        set.Add(e1);
        set.Add(e2); // Should not add duplicate
        set.Add(e3);

        set.Should().HaveCount(2);
        set.Should().Contain(e1);
        set.Should().Contain(e3);
    }
}
