using Godot;

namespace Quad.scripts;

public enum CellType
{
    GROUND,
    WALL
}

public partial class Cell : Node2D
{
    [Export] public Sprite2D Sprite;

    public CellType Type
    {
        set
        {
            switch (value)
            {
                case CellType.GROUND:
                    break;
                case CellType.WALL:
                    break;
                default:
                    Logger.Error("Trying to set cell to invalid type");
                    break;
            }
        }
    }
}