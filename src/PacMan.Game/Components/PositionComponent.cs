using PacMan.ECS;

namespace PacMan.Game.Components;

public record struct PositionComponent((int X, int Y) Position) : IComponent;