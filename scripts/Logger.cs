using Godot;

namespace Quad.scripts;

public partial class Logger : Node2D
{
    public static void Info(string message)
    {
        GD.PrintRich($"[color=white][b][INFO][/b][/color] {message}");
    }

    public static void Done()
    {
        Info("Done!");
    }

    public static void Warn(string message)
    {
        GD.PrintRich($"[color=orange][b][WARN][/b][/color] {message}");
    }

    public static void Error(string message)
    {
        GD.PrintRich($"[color=red][b][ERROR][/b][/color] {message}");
    }
}