using Godot.Collections;

namespace Quad.scripts;

public class PlayerInfo
{
    public readonly string Test;
    public string Name;

    public PlayerInfo(Array<string> content)
    {
        Name = content[(int)Fields.Name];
        Test = content[(int)Fields.Test];
    }

    public PlayerInfo(string name, string test)
    {
        Name = name;
        Test = test;
    }

    public Array<string> Content => [Name, Test];

    private enum Fields
    {
        Name,
        Test
    }
}