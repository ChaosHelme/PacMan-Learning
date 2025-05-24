using PacMan.Game.Ecs;
using PacMan.Game.Systems;

namespace PacMan.Game.Components;

public record struct InputComponent(Direction Direction) : IUniqueComponent;