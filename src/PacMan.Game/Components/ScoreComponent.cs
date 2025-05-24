using PacMan.ECS;

namespace PacMan.Game.Components;

public record struct ScoreComponent(int Score) : IComponent;