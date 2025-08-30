using Godot;
using Quad.scenes.menu.screens;

namespace Quad.scripts;

public partial class Menu : Node
{
    [Export] private TitleScreen _titleScreen;
    [Export] private LobbyScreen _lobbyScreen;
    [Export] private ConnectionScreen _connectionScreen;
    [Export] private PendingConnectionScreen _pendingConnectionScreen;
    
    private Control _currentScreen;

    public static Menu Instance { get; private set; }

    public override void _Ready()
    {

        Instance = this;
        //NETWORKED TRIGGERED SIGNALS
        OnlineLobby.Instance.PlayerConnected += (_, _) => _lobbyScreen.UpdatePlayerList();
        OnlineLobby.Instance.PlayerDisconnected += _ => _lobbyScreen.UpdatePlayerList();
        OnlineLobby.Instance.ServerDisconnected += OnServerDiconnected;
        //LOCAL NETWORK SIGNALS
        OnlineLobby.Instance.ConnectionSuccessful += OnConnectionSuccessful;
        OnlineLobby.Instance.ConnectionFailed += OnConnectionFailed;

        GotoTitle();
    }
    
    private void SetCurrentScreen(Control screen)
    {
        if (_currentScreen != null) _currentScreen.Visible = false;

        screen.Visible = true;
        _currentScreen = screen;
    }

    public void GotoTitle()
    {
        SetCurrentScreen(_titleScreen);
    }
    
    public void GotoLobby()
    {
        SetCurrentScreen(_lobbyScreen);
    }

    public void GotoConnection()
    {
        SetCurrentScreen(_connectionScreen);
    }

    public void GotoPendingConnection()
    {
        SetCurrentScreen(_pendingConnectionScreen);
    }
    
    //SIGNALS
    private void OnServerDiconnected()
    {
        OnlineLobby.Instance.ClearMultiplayer();
        SetCurrentScreen(_titleScreen);
    }
    
    private void OnConnectionFailed()
    {
        SetCurrentScreen(_connectionScreen);
    }

    private void OnConnectionSuccessful()
    {
        _lobbyScreen.UpdatePlayerList();
        GotoLobby();
    }
}