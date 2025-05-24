using PacMan.Ecs.Console.Systems;

namespace PacMan.Ecs.Console.Components;

public sealed record InputComponent(Direction Direction) : IUniqueComponent;