namespace PacMan.Game.Ecs;

public readonly struct Entity(int id) : IEquatable<Entity>
{
    public int Id { get; } = id;
    public override int GetHashCode() => Id;
    public override bool Equals(object? obj) => obj is Entity e && e.Id == Id;

    public bool Equals(Entity other) => Id == other.Id;
}