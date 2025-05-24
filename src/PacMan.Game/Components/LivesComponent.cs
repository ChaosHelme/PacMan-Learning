using PacMan.ECS;

namespace PacMan.Game.Components;

public record struct LivesComponent(int Lives) : IComponent;