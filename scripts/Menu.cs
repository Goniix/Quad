using System.Linq;
using Godot;

namespace Quad.scripts;

public partial class Menu : Node
{
    private Control _currentScreen;
    [Export] private Button _hostButton;
    [Export] private Button _joinButton;
    [Export] private Button _leaveLobbyButton;
    [Export] private Control _lobbyScreen;
    [Export] private LobbyPortraitsArray _portraitsArray;
    [Export] private Control _titleScreen;


    public override void _Ready()
    {
        OnlineLobby.Instance.PlayerConnected += (_, _) => UpdatePlayerList();
        OnlineLobby.Instance.PlayerDisconnected += _ => UpdatePlayerList();
        OnlineLobby.Instance.ServerDisconnected += OnServerDiconnected;


        _hostButton!.Pressed += OnCreateLobbyButtonPressed;
        _joinButton!.Pressed += OnJoinLobbyButtonPressed;
        _leaveLobbyButton.Pressed += OnLeaveLobbyButtonPressed;

        SetCurrentScreen(_titleScreen);
    }

    private void UpdatePlayerList()
    {
        var playerList = OnlineLobby.Instance.Players.Values.ToList();
        var nameList = new string[playerList.Count];
        for (var i = 0; i < playerList.Count; i++) nameList[i] = playerList[i].Name;

        _portraitsArray.Names = nameList.OrderBy(s => s).ToArray();
    }

    private void SetCurrentScreen(Control screen)
    {
        if (_currentScreen != null) _currentScreen.Visible = false;

        screen.Visible = true;
        _currentScreen = screen;
    }

    private void OnLeaveLobbyButtonPressed()
    {
        OnlineLobby.Instance.ClearMultiplayer(); //MAKE LEAVE REQUEST INSTEAD
        SetCurrentScreen(_titleScreen);
    }

    private void OnCreateLobbyButtonPressed()
    {
        OnlineLobby.Instance.CreateGame();
        SetCurrentScreen(_lobbyScreen);
    }

    private void OnJoinLobbyButtonPressed()
    {
        OnlineLobby.Instance.JoinGame();
        SetCurrentScreen(_lobbyScreen);
    }

    private void OnServerDiconnected()
    {
        OnlineLobby.Instance.ClearMultiplayer();
        SetCurrentScreen(_titleScreen);
    }
}