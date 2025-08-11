using System.Linq;
using Godot;

namespace Quad.scripts;

public partial class LobbyPortraitsArray : Control
{
    [Export] private LobbyPortrait[] _portraits = new LobbyPortrait[4];

    public void UpdatePortraits()
    {
        Logger.Info(_portraits.Length.ToString());
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