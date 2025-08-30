using Godot;
using Quad.scripts;

namespace Quad.scenes.menu.screens;

public partial class LobbyScreen : Control
{
    [Export] private Button _leaveButton;
    [Export] private LobbyPortraitsArray _portraitsArray;
    public override void _Ready()
    {
        //LOBBY
        _leaveButton.Pressed += OnLeaveLobbyButtonPressed;
    }
    
    private void OnLeaveLobbyButtonPressed()
    {
        OnlineLobby.Instance.ClearMultiplayer(); //MAKE LEAVE REQUEST INSTEAD
        Menu.Instance.GotoTitle();
    }
    
    public void UpdatePlayerList()
    {
        _portraitsArray.UpdatePortraits();
    }
}