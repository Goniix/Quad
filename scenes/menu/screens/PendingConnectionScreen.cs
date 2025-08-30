using Godot;
using Quad.scripts;

namespace Quad.scenes.menu.screens;

public partial class PendingConnectionScreen : Control
{
    [Export] private Label _statusLabel;
    [Export] private Button _cancelButton;
    public override void _Ready()
    {
        _cancelButton.Pressed += () =>
        {
            OnlineLobby.Instance.ClearMultiplayer();
            Menu.Instance.GotoConnection();
        };
    }
}