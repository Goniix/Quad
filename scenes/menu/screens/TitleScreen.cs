using Godot;
using Quad.scripts;

namespace Quad.scenes.menu.screens;



public partial class TitleScreen : Control
{
    [Export] private Button _hostButton;
    [Export] private Button _joinButton;

    public override void _Ready()
    {
        _hostButton!.Pressed += OnCreateLobbyButtonPressed;
        _joinButton!.Pressed += OnJoinLobbyButtonPressed;
    }
    
    private void OnCreateLobbyButtonPressed()
    {
        OnlineLobby.Instance.CreateGame();
        Menu.Instance.GotoLobby();
    }

    private void OnJoinLobbyButtonPressed()
    {
        Menu.Instance.GotoConnection();

    }
}