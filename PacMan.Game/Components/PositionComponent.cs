using PacMan.Game.Ecs;

namespace PacMan.Game.Components;

public record struct PositionComponent(int X, int Y) : IComponent;