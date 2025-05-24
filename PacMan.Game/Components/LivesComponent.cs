using PacMan.Game.Ecs;

namespace PacMan.Game.Components;

public record struct LivesComponent(int Lives) : IComponent;