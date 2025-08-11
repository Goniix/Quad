using System.Collections.Generic;
using Godot;
using Godot.Collections;

namespace Quad.scripts;

public partial class OnlineLobby : Node
{
    [Signal]
    public delegate void LobbyUpdatedEventHandler();

    // These signals can be connected to by a UI lobby scene or the game scene.
    [Signal]
    public delegate void PlayerConnectedEventHandler(int peerId, Array<string> playerInfo);

    [Signal]
    public delegate void PlayerDisconnectedEventHandler(int peerId);

    [Signal]
    public delegate void ServerDisconnectedEventHandler();

    private const int Port = 7000;
    private const string DefaultServerIP = "127.0.0.1"; // IPv4 localhost
    private const int MaxConnections = 4;

    //EDIT WITH TRUE PLAYER INFO BEFORE JOIN/HOST
    public readonly PlayerInfo LocalPlayerInfo = new("NONAME", "Zub");

    // This will contain player info for every player,
    // with the keys being each player's unique IDs.
    public readonly SortedDictionary<long, PlayerInfo> Players = new();

    private int _playersLoaded;
    public static OnlineLobby Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
        Multiplayer.PeerConnected += OnPlayerConnected;
        Multiplayer.PeerDisconnected += OnPlayerDisconnected;
        Multiplayer.ConnectedToServer += OnConnectOk; //self call
        Multiplayer.ConnectionFailed += OnConnectionFail; //self call
        Multiplayer.ServerDisconnected += OnServerDisconnected;
    }

    public Error JoinGame(string address = "")
    {
        if (string.IsNullOrEmpty(address)) address = DefaultServerIP;

        var peer = new ENetMultiplayerPeer();
        var error = peer.CreateClient(address, Port);

        if (error != Error.Ok) return error;

        Multiplayer.MultiplayerPeer = peer;
        return Error.Ok;
    }

    public Error CreateGame()
    {
        var peer = new ENetMultiplayerPeer();
        var error = peer.CreateServer(Port, MaxConnections);
        if (error != Error.Ok) return error;

        Multiplayer.MultiplayerPeer = peer;
        Players[1] = LocalPlayerInfo;
        EmitSignal(SignalName.PlayerConnected, 1, Players[1].Content);
        return Error.Ok;
    }

    public void ClearMultiplayer()
    {
        if (Multiplayer.MultiplayerPeer != null && Multiplayer.IsServer()) Multiplayer.MultiplayerPeer.Close();
        Multiplayer.MultiplayerPeer = null;
        Players.Clear();
    }

    // When the server decides to start the game from a UI scene,
    // do Rpc(Lobby.MethodName.LoadGame, filePath);
    [Rpc(CallLocal = true, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
    private void LoadGame(string gameScenePath)
    {
        GetTree().ChangeSceneToFile(gameScenePath);
    }

    // Every peer will call this when they have loaded the game scene.
    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
    private void PlayerLoaded()
    {
        if (Multiplayer.IsServer())
        {
            _playersLoaded += 1;
            if (_playersLoaded == Players.Count)
                // GetNode<Game>("/root/Game").StartGame();
                _playersLoaded = 0;
        }
    }

    // When a peer connects, send them my player info.
    // This allows transfer of all desired data for each player, not only the unique ID.
    private void OnPlayerConnected(long id)
    {
        RpcId(id, MethodName.RegisterPlayer, LocalPlayerInfo.Content);
    }

    [Rpc(MultiplayerApi.RpcMode.AnyPeer, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
    private void RegisterPlayer(Array<string> newPlayerInfo)
    {
        var newPlayerId = Multiplayer.GetRemoteSenderId();
        Players[newPlayerId] = new PlayerInfo(newPlayerInfo);
        EmitSignal(SignalName.PlayerConnected, newPlayerId, newPlayerInfo);
    }

    private void OnPlayerDisconnected(long id)
    {
        Players.Remove(id);
        EmitSignal(SignalName.PlayerDisconnected, id);
    }

    private void OnConnectOk() //RUNS ON SELF
    {
        var peerId = Multiplayer.GetUniqueId();
        Players[peerId] = LocalPlayerInfo;
        EmitSignal(SignalName.PlayerConnected, peerId, LocalPlayerInfo.Content);
    }

    private void OnConnectionFail() //RUNS ON SELF
    {
        ClearMultiplayer();
    }

    private void OnServerDisconnected()
    {
        ClearMultiplayer();
        EmitSignal(SignalName.ServerDisconnected);
    }
}