using Godot;
using Quad.scripts;

namespace Quad.scenes.menu.screens;

public partial class ConnectionScreen : Control
{
    [Export] private Button _leaveButton;
    [Export] private LineEdit _ipEdit;
    [Export] private Button _connectButton;
    public override void _Ready()
    {
        _leaveButton.Pressed += () => { Menu.Instance.GotoTitle(); };
        _connectButton.Pressed += OnConnectButtonPressed;
    }
    
    private void OnConnectButtonPressed()
    {
        OnlineLobby.Instance.JoinGame(_ipEdit.Text);
        Menu.Instance.GotoPendingConnection();
    }
}