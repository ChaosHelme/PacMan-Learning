using PacMan.Game.Ecs;

namespace PacMan.Game.Components;

public record struct ScoreComponent(int Score) : IComponent;