using Godot;

namespace Quad.scripts;

public partial class Logger : Node2D
{
    public enum Source
    {
        Net,
        None
    }

    public static void Info(string message, Source source = Source.None)
    {
        var tag = GetTagForSource(source);
        GD.PrintRich($"{tag}[color=white][b][INFO][/b][/color] {message}");
    }

    public static void Done()
    {
        Info("Done!");
    }

    public static void Warn(string message, Source source = Source.None)
    {
        var tag = GetTagForSource(source);
        GD.PrintRich($"{tag}[color=orange][b][WARN][/b][/color] {message}");
    }

    public static void Error(string message, Source source = Source.None)
    {
        // var tag = GetTagForSource(source);
        // GD.PrintRich($"{tag}[color=red][b][ERROR][/b][/color] {message}");
        GD.PrintErr(message);
    }

    private static string GetTagForSource(Source source)
    {
        return source switch
        {
            Source.Net => "[color=cyan][b][NET][/b][/color]",
            Source.None => "",
            _ => source.ToString()
        };
    }
}