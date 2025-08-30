using System.Collections.Generic;
using Godot;

namespace Quad.scripts;

public partial class Grid : TileMapLayer
{
    private readonly Dictionary<Vector2I, Cell> _cells = new();


    public override void _Ready()
    {
        InitCellFromTileMapLayer();
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("Click"))
        {
            if (@event is InputEventMouseButton mouseEvent)
                GD.Print(LocalToMap(ToLocal(mouseEvent.GlobalPosition)));

            Logger.Info("Cliked");
        }
    }

    public Cell GetCell(Vector2I position)
    {
        return _cells[position];
    }

    private void InitCellFromTileMapLayer()
    {
        Logger.Info("Initializing cell grid from TileMapLayer");
        foreach (var tile in GetUsedCells()) _cells[tile] = new Cell();
        Logger.Done();
    }
}