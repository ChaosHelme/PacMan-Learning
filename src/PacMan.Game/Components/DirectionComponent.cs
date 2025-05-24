using PacMan.ECS;
using PacMan.Game.Systems;

namespace PacMan.Game.Components;

public record struct DirectionComponent(Direction Direction) : IComponent;