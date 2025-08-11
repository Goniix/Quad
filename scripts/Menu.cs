using Godot;

namespace Quad.scripts;

public partial class Menu : Node
{
    [Export] private Control _titleScreen;
    [Export] private Button _hostButton;
    [Export] private Button _joinButton;

    [Export] private Control _lobbyScreen;
    [Export] private Button _leaveLobbyButton;
    [Export] private LobbyPortraitsArray _portraitsArray;

    [Export] private Control _connectionScreen;
    [Export] private Button _leaveConnectionButton;
    [Export] private LineEdit _connectIpEdit;
    [Export] private Button _connectButton;

    [Export] private Control _pendingConnectionScreen;
    [Export] private Label _pendingConnectionStatus;
    [Export] private Button _pendingCancelButton;

    private Control _currentScreen;

    public override void _Ready()
    {
        //NETWORKED TRIGGERED SIGNALS
        OnlineLobby.Instance.PlayerConnected += (_, _) => UpdatePlayerList();
        OnlineLobby.Instance.PlayerDisconnected += _ => UpdatePlayerList();
        OnlineLobby.Instance.ServerDisconnected += OnServerDiconnected;
        //LOCAL NETWORK SIGNALS
        OnlineLobby.Instance.ConnectionSuccessful += OnConnectionSuccessful;
        OnlineLobby.Instance.ConnectionFailed += OnConnectionFailed;

        //TITLE
        _hostButton!.Pressed += OnCreateLobbyButtonPressed;
        _joinButton!.Pressed += OnJoinLobbyButtonPressed;

        //LOBBY
        _leaveLobbyButton.Pressed += OnLeaveLobbyButtonPressed;

        //CONNECTION
        _leaveConnectionButton.Pressed += () => { SetCurrentScreen(_titleScreen); };
        _connectButton.Pressed += OnConnectButtonPressed;

        //PENDING CONNECTION
        _pendingCancelButton.Pressed += () =>
        {
            OnlineLobby.Instance.ClearMultiplayer();
            SetCurrentScreen(_connectionScreen);
        };

        SetCurrentScreen(_titleScreen);
    }

    private void UpdatePlayerList()
    {
        _portraitsArray.UpdatePortraits();
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
        SetCurrentScreen(_connectionScreen);
    }

    private void OnServerDiconnected()
    {
        OnlineLobby.Instance.ClearMultiplayer();
        SetCurrentScreen(_titleScreen);
    }

    private void OnConnectButtonPressed()
    {
        OnlineLobby.Instance.JoinGame(_connectIpEdit.Text);
        SetCurrentScreen(_pendingConnectionScreen);
    }

    private void OnConnectionFailed()
    {
        SetCurrentScreen(_connectionScreen);
    }

    private void OnConnectionSuccessful()
    {
        UpdatePlayerList();
        SetCurrentScreen(_lobbyScreen);
    }
}