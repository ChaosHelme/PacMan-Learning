namespace PacMan.ECS;

public class World
{
	private int _nextEntityId = 0;
	private readonly Queue<int> _freeIds = new();
	private readonly Dictionary<int, int> _entityVersions = new();
	private readonly Dictionary<Type, IComponentPool> _componentPools = new();
    
	public Entity CreateEntity()
	{
		int id;
		if (_freeIds.Count > 0)
		{
			id = _freeIds.Dequeue();
			_entityVersions[id]++;
		}
		else
		{
			id = _nextEntityId++;
			_entityVersions[id] = 1;
		}
		return new Entity(id, _entityVersions[id]);
	}

	public void AddComponent<T>(Entity entity, T component) where T : IComponent
	{
		var pool = GetOrCreatePool<T>();
		if (pool.Has(entity.Id))
		{
			throw new InvalidOperationException();
		}
		pool.Add(entity.Id, component);
	}

	public T GetComponent<T>(Entity entity) where T : IComponent
	{
		var pool = GetOrCreatePool<T>();
		return pool.Get(entity.Id);
	}

	public void RemoveComponent<T>(Entity entity) where T : IComponent
	{
		var pool = GetOrCreatePool<T>();
		pool.Remove(entity.Id);
	}

	private ComponentPool<T> GetOrCreatePool<T>() where T : IComponent
	{
		if (!_componentPools.TryGetValue(typeof(T), out var pool))
		{
			pool = new ComponentPool<T>();
			_componentPools[typeof(T)] = pool;
		}
		return (ComponentPool<T>)pool;
	}

    
    public void ReplaceComponent<T>(Entity entity, T component) where T : IComponent
    {
        // Remove if exists (including unique tracking)
        RemoveComponent<T>(entity);

        // Add new component (AddComponent will enforce unique constraints)
        AddComponent(entity, component);
    }

    public bool HasComponent<T>(Entity entity) where T : IComponent
	{
		var pool = GetOrCreatePool<T>();
		return pool.Has(entity.Id);
	}

	public IEnumerable<Entity> GetEntitiesWith<T>()
		where T : IComponent
	{
		var pool = GetOrCreatePool<T>();
		return pool.Entities.Select(id => new Entity(id, _entityVersions[id]));
	}

	public IEnumerable<Entity> GetEntitiesWith<T1, T2>()
		where T1 : IComponent
		where T2 : IComponent
	{
		var pool1 = GetOrCreatePool<T1>();
		var pool2 = GetOrCreatePool<T2>();
		return pool1.Entities.Intersect(pool2.Entities)
			.Select(id => new Entity(id, _entityVersions[id]));
	}

    
    /// <summary>
    /// Destroys the given entity, removing all its components.
    /// </summary>
	public void DestroyEntity(Entity entity)
	{
		foreach (var pool in _componentPools.Values)
			pool.Remove(entity.Id);

		_freeIds.Enqueue(entity.Id);
	}
}

public class EntityDoesNotExistsException : Exception
{
    public EntityDoesNotExistsException(string message) : base(message) { }
}
