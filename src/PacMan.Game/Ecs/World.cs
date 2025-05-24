namespace PacMan.Game.Ecs;

public class World
{
    private int _nextEntity = 1;
    private readonly Dictionary<Type, Dictionary<Entity, IComponent>> _components = new();
    private readonly Dictionary<Type, Entity> _uniqueComponentOwners = new();
    
    public Entity CreateEntity() => new(_nextEntity++);

    public void AddComponent<T>(Entity entity, T component) where T : IComponent
    {
        var type = typeof(T);
        
        EnsureUniqueness<T>(entity, type);

        if (!_components.TryGetValue(type, out var dict))
            _components[type] = dict = new Dictionary<Entity, IComponent>();

        if (!dict.TryAdd(entity, component))
            throw new InvalidOperationException(
                $"Entity {entity.Id} already has a component of type {type.Name}.");
    }
    
    public void ReplaceComponent<T>(Entity entity, T component) where T : IComponent
    {
        // Remove if exists (including unique tracking)
        RemoveComponent<T>(entity);

        // Add new component (AddComponent will enforce unique constraints)
        AddComponent(entity, component);
    }

    void EnsureUniqueness<T>(Entity entity, Type type)
        where T : IComponent
    {
        // Unique component logic: ensure only one exists
        if (typeof(IUniqueComponent).IsAssignableFrom(type))
        {
            if (_uniqueComponentOwners.TryGetValue(type, out var owner) && !owner.Equals(entity))
                throw new InvalidOperationException(
                    $"Component of type {type.Name} is unique and already exists on entity {owner.Id}.");
            _uniqueComponentOwners[type] = entity;
        }
    }

    public T GetComponent<T>(Entity entity) where T : IComponent
        => (T)_components[typeof(T)][entity];

    public bool HasComponent<T>(Entity entity) where T : IComponent
        => _components.TryGetValue(typeof(T), out var dict) && dict.ContainsKey(entity);

    public IEnumerable<Entity> GetEntitiesWith<T>() where T : IComponent
        => _components.TryGetValue(typeof(T), out var dict) ? dict.Keys : Enumerable.Empty<Entity>();

    public IEnumerable<Entity> GetEntitiesWith<T1, T2>()
        where T1 : IComponent where T2 : IComponent
        => GetEntitiesWith<T1>().Intersect(GetEntitiesWith<T2>());

    public void RemoveComponent<T>(Entity entity) where T : IComponent
    {
        var type = typeof(T);

        if (_components.TryGetValue(type, out var dict))
        {
            dict.Remove(entity);

            // Clean up unique component tracking
            if (typeof(IUniqueComponent).IsAssignableFrom(type) &&
                _uniqueComponentOwners.TryGetValue(type, out var owner) &&
                owner.Equals(entity))
            {
                _uniqueComponentOwners.Remove(type);
            }
        }
    }
    
    public Entity GetUniqueComponentOwner<T>() where T : IUniqueComponent
    {
        if (_uniqueComponentOwners.TryGetValue(typeof(T), out var entity))
            return entity;
        throw new EntityDoesNotExistsException($"There is no entity which contains a unique component of type {typeof(T).Name}");
    }
}

public class EntityDoesNotExistsException : Exception
{
    public EntityDoesNotExistsException(string message) : base(message) { }
}
