namespace PacMan.ECS;

public interface IComponentPool
{
	void Remove(int entityId);
}

public class ComponentPool<T> : IComponentPool where T : IComponent
{
	private readonly Dictionary<int, T> _components = new();

	public void Add(int entityId, T component) => _components[entityId] = component;
	public bool Has(int entityId) => _components.ContainsKey(entityId);
	public T Get(int entityId) => _components[entityId];
	public void Remove(int entityId) => _components.Remove(entityId);
	public IEnumerable<int> Entities => _components.Keys;
}
