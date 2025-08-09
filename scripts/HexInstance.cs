using Godot;

namespace Quad.scripts;

public partial class HexInstance : Node2D
{
    public Vector2I HexPos
    {
        get => Game.Instance.GridScene.LocalToMap(Position);

        set => Position = Game.Instance.GridScene.MapToLocal(value);
    }
}