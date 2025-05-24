using PacMan.Ecs.Console.Systems;

namespace PacMan.Ecs.Console.Components;

public sealed record DirectionComponent(Direction Value) : IComponent;