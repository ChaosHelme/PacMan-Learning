namespace PacMan.Ecs.Console;

public class World
{
    private int _nextEntity = 1;
    private readonly Dictionary<Type, Dictionary<Entity, IComponent>> _components = new();

    public Entity CreateEntity() => new Entity(_nextEntity++);

    public void AddComponent<T>(Entity entity, T component) where T : IComponent
    {
        if (!_components.TryGetValue(typeof(T), out var dict))
            _components[typeof(T)] = dict = new();
        dict[entity] = component;
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
        if (!_components.TryGetValue(typeof(T), out var dict))
        {
            throw new InvalidOperationException($"Component {typeof(T)} does not exist on entity {entity}");
        }
        dict.Remove(entity);
    }
}
