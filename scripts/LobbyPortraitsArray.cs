using System.Linq;
using Godot;

namespace Quad.scripts;

public partial class LobbyPortraitsArray : Control
{
    private LobbyPortrait[] _portraits = new LobbyPortrait[4];

    public override void _Ready()
    {
        for (var i = 0; i < 4; i++)
        {
            _portraits[i] = GetNode<LobbyPortrait>($"portrait{i}");
        }
    }

    public void UpdatePortraits()
    {
        var infos = OnlineLobby.Instance.Players;
        var keys = infos.Keys.ToArray();
        var names = infos.Values.ToArray();
        for (var i = 0; i < 4; i++)
        {
            string name;
            if (i < infos.Count)
                name = keys[i] == Multiplayer.GetUniqueId() ? $"[color=yellow]{names[i].Name}[/color]" : names[i].Name;
            else
                name = "[color=gray]<Empty>[/color]";

            _portraits[i].PlayerName.Text = name;
        }
    }
}