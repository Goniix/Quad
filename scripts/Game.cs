using Godot;

namespace Quad.scripts;

public partial class Game : Node
{
    [Export] public Grid GridScene { get; private set; }
    [Export] public PointLight2D PointLight { get; private set; }
    public static Game Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
        if (GridScene is null) Logger.Error("GridScene is null");
        if (PointLight is null) Logger.Error("PointLight is null");

        if (GetNode("Player") is Player player) player.HexPos = new Vector2I(12, 3);
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion mouseMotion) PointLight.GlobalPosition = mouseMotion.GlobalPosition;
    }
}