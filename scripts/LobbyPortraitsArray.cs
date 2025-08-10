using Godot;

namespace Quad.scripts;

public partial class LobbyPortraitsArray : Control
{
    public string[] Names
    {
        set
        {
            for (var i = 0; i < 4; i++)
                if (GetNode<Control>("./PortraitPlayer" + i).GetNode<RichTextLabel>("Label") is { } label)
                    label.Text = i < value.Length ? value[i] : "[color=red]DEFAULT TEXT[/color]";
        }
    }
}